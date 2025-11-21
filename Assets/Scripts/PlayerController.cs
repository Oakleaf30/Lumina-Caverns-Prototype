using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float verticalOffset = -0.5f;

    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb;
    private Vector2 lastDirection = Vector2.down; // 1. Initialize with a default so you can interact at start

    public Grid grid;
    public Tilemap interactionTilemap;

    private Dictionary<Vector3Int, int> tileHealthMap = new Dictionary<Vector3Int, int>();

    [Header("Interactable Tiles")]
    public TileBase anvilBottomTile;
    public TileBase craftingTile;
    public TileBase bookTile;
    public TileBase ladderTile;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetMouseButtonDown(0))
        {
            MineBlock(targetCell);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CheckInteraction(targetCell);
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
            tileHealthMap[targetCell]--;

            // 3. Check for destruction using the temporary value
            if (tileHealthMap[targetCell] <= 0)
            {
                interactionTilemap.SetTile(targetCell, null);
                // Crucial: Remove the tile from the dictionary when destroyed
                tileHealthMap.Remove(targetCell);
            }
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
            Debug.Log("Interacting with Anvil!");
        }
        else if (targetTile == craftingTile)
        {
            Debug.Log("Interacting with Crafting Table!");
        }
        else if (targetTile == bookTile)
        {
            Debug.Log("Interacting with Book!");
        }
        else if (targetTile == ladderTile)
        {
            Debug.Log("Interacting with Ladder!");
        }
    }
}