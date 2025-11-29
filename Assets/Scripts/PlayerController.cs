using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float verticalOffset = -0.5f;
    public int pickaxeDamage => Durability.Instance.pickaxeDamage;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection = Vector2.right; // 1. Initialize with a default so you can interact at start
    private Light2D light2d;

    public Grid grid;
    public Tilemap interactionTilemap;
    public GameObject tutorialUI;
    public GameObject resourceUI;
    public GameObject craftingUI;
    public GameObject forgeUI;

    public PlayerAttack attack;
    public HealthDisplay healthDisplay;
    public GameObject gateWarning;

    private Dictionary<Vector3Int, int> tileHealthMap = new Dictionary<Vector3Int, int>();

    [Header("Interactable Tiles")]
    public TileBase anvilBottomTile;
    public TileBase craftingTile;
    public TileBase bookTile;
    public TileBase chestTile;
    public TileBase ladderTile;
    public TileBase gateTile;

    private int rocksBroken = 0;
    public bool invincible = false;

    private string CurrentSceneName => SceneManager.GetActiveScene().name;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        light2d = GetComponent<Light2D>();

        // 2. ONLY proceed if the light2d component was actually found (is not null).
        if (light2d != null)
        {
            // Now it's safe to access its properties without crashing.
            light2d.pointLightOuterRadius = EnchantManager.Instance.lightStrength;
        }
        moveSpeed = EnchantManager.Instance.moveSpeed;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 feetPosition = transform.position + new Vector3(0, verticalOffset, 0);
        Vector3Int playerCell = grid.WorldToCell(feetPosition);

        // Calculate movement direction
        Vector2 currentInput = new Vector2(horizontalInput, verticalInput);

        // 2. FIX: Determine the facing direction more strictly
        Vector3Int facingOffset;

        if (currentInput.sqrMagnitude > 0.1f) // If we are moving
        {
            // Normalize to handle diagonals gracefully for movement
            Vector2 normalized = currentInput.normalized;

            // Update last direction
            lastDirection = normalized;

            // 3. FORCE CARDINAL INTERACTION: 
            // If X pull is stronger than Y, face Horizontal. Otherwise face Vertical.
            // This prevents the "Diagonal Miss" problem.
            if (Mathf.Abs(normalized.x) > Mathf.Abs(normalized.y))
            {
                facingOffset = new Vector3Int(Mathf.RoundToInt(Mathf.Sign(normalized.x)), 0, 0);
            }
            else
            {
                facingOffset = new Vector3Int(0, Mathf.RoundToInt(Mathf.Sign(normalized.y)), 0);
            }
        }
        else
        {
            // If not moving, use the last stored direction (also enforcing cardinal)
            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
            {
                facingOffset = new Vector3Int(Mathf.RoundToInt(Mathf.Sign(lastDirection.x)), 0, 0);
            }
            else
            {
                facingOffset = new Vector3Int(0, Mathf.RoundToInt(Mathf.Sign(lastDirection.y)), 0);
            }
        }

        Vector3Int targetCell = playerCell + facingOffset;

        // 4. VISUAL DEBUG: Draw a line in the Scene View to show where you are aiming
        Vector3 debugStart = grid.CellToWorld(playerCell) + new Vector3(0.5f, 0.5f, 0);
        Vector3 debugEnd = grid.CellToWorld(targetCell) + new Vector3(0.5f, 0.5f, 0);
        Debug.DrawLine(debugStart, debugEnd, Color.red);

        if (Input.GetMouseButtonDown(0) && CurrentSceneName == "Mine")
        {
            if (Durability.Instance.durability > 0) MineBlock(targetCell);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CheckInteraction(targetCell);
        }

        if (Input.GetKeyDown(KeyCode.E) && CurrentSceneName == "Mine") {
            resourceUI.SetActive(!resourceUI.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            attack.Attack(lastDirection);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Base");
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(horizontalInput, verticalInput);
        moveVector.Normalize();
        rb.linearVelocity = moveVector * moveSpeed; // changed from linearVelocity (Unity 6) to velocity (Standard)
    }

    void MineBlock(Vector3Int targetCell)
    {
        TileBase targetTile = interactionTilemap.GetTile(targetCell);

        if (targetTile is ResourceTile resource)
        {
            // 1. Check if the tile is ALREADY being tracked
            if (!tileHealthMap.ContainsKey(targetCell))
            {
                // If not, initialize its health using the max value from the asset
                tileHealthMap.Add(targetCell, resource.maxHitPoints);
            }

            // 2. Reduce the temporary HP stored in the dictionary
            tileHealthMap[targetCell] -= pickaxeDamage;
            Durability.Instance.DamagePickaxe();

            // 3. Check for destruction using the temporary value
            if (tileHealthMap[targetCell] <= 0)
            {
                BlockMined(targetCell, resource);
            }
        }
    }

    void BlockMined(Vector3Int targetCell, ResourceTile targetTile)
    {
        interactionTilemap.SetTile(targetCell, null);
        // Crucial: Remove the tile from the dictionary when destroyed
        tileHealthMap.Remove(targetCell);
        rocksBroken++;

        RockSpawner.Instance.LadderCheck(targetCell, rocksBroken);

        string type = targetTile.resourceID;
        int amount = RollBonusOre();
        Inventory.Instance.AddResource(type, amount);
    }

    int RollBonusOre()
    {
        if (EnchantManager.Instance.enchantDic["Ores"])
        {
            int bonus = Random.Range(1, 3);
            return bonus;
        }
        else
        {
            return 1;
        }
    }

    void CheckInteraction(Vector3Int targetCell)
    {
        TileBase targetTile = interactionTilemap.GetTile(targetCell);

        // Debug check to see what we actually hit
        if (targetTile == null)
        {
            // Debug.Log("Hit nothing at: " + targetCell);
            return;
        }

        if (targetTile == anvilBottomTile)
        {
            forgeUI.SetActive(!forgeUI.activeInHierarchy);
        }
        else if (targetTile == craftingTile)
        {
            craftingUI.SetActive(!craftingUI.activeInHierarchy);
        }
        else if (targetTile == bookTile)
        {
            tutorialUI.SetActive(!tutorialUI.activeInHierarchy);
        }
        else if (targetTile == chestTile)
        {
            resourceUI.SetActive(!resourceUI.activeInHierarchy);
        }
        else if (targetTile == ladderTile)
        {
            SceneManager.LoadScene("Mine");
        } else if (targetTile == gateTile)
        {
            if (RockSpawner.Instance.keyObtained)
            {
                SceneManager.LoadScene("End");
            } else
            {
                Instantiate(gateWarning);
            }
        }
    }

    IEnumerator Invincible()
    {
        invincible = true;
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invincible = false;
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(Invincible());
        PlayerHealth.Instance.currentHealth -= damage;
        healthDisplay.UpdateDisplay();

        if (PlayerHealth.Instance.currentHealth <= 0)
        {
            Inventory.Instance.ResourcesLost();
            SceneManager.LoadScene("Base");
            Durability.Instance.durability = Durability.Instance.maxDurability;
        }
    }
}