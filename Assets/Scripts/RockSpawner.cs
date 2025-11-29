using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class RockSpawner : MonoBehaviour
{
    public static RockSpawner Instance;

    // Assign in Inspector: The Tilemap you are spawning rocks onto (Ore_Layer)
    public Tilemap interactionTilemap;

    // Assign in Inspector: The tile asset for the rock/ore
    public TileBase rockTile;
    public TileBase rockTile2;
    public TileBase copperTile;
    public TileBase ironTile;
    public TileBase amethystTile;
    public TileBase rubyTile;
    public TileBase keyTile;
    public TileBase ladderTile;
    public TileBase gateTile;

    // Define the area where rocks can spawn (e.g., area 0 to 50 on X and Y)
    public Vector2Int spawnAreaMin = new Vector2Int(-8, -4);
    public Vector2Int spawnAreaMax = new Vector2Int(8, 4);

    // How many rocks to spawn initially
    public int initialRockCount;

    public int level = 1;

    public bool keyObtained = false;
    private bool keySpawned = false;
    private int keyChance;

    private bool ladderSpawned = false;

    // Inside the RockSpawner script:

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Start()
    {
        SpawnRocks(initialRockCount);
    }

    public void SpawnRocks(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Get a random grid cell within the defined bounds
            Vector3Int randomCell = GetRandomSpawnCell();
            TileBase tile = SelectTile();

            // Check if the cell is currently empty
            if (CanSpawnAt(randomCell) && randomCell != Vector3Int.zero)
            {
                // Place the rock tile
                interactionTilemap.SetTile(randomCell, tile);
            }
        }
    }

    // Inside the RockSpawner script:

    Vector3Int GetRandomSpawnCell()
    {
        int x = Random.Range(spawnAreaMin.x, spawnAreaMax.x + 1);
        int y = Random.Range(spawnAreaMin.y, spawnAreaMax.y + 1);

        // Z is 0 for 2D tilemaps
        return new Vector3Int(x, y, 0);
    }

    // Inside the RockSpawner script:

    bool CanSpawnAt(Vector3Int cell)
    {
        // Check the interaction layer (Ore_Layer)
        TileBase existingTile = interactionTilemap.GetTile(cell);

        // Check the floor layer to ensure we are only spawning on the floor (optional, but recommended)
        // You would need a public reference to the floor Tilemap here: public Tilemap floorTilemap;
        // TileBase floorTile = floorTilemap.GetTile(cell);

        // If existingTile is null, the cell is empty on the interaction layer.
        return existingTile == null;

        // Add logic here if you need to ensure the floor is present:
        // return existingTile == null && floorTile != null;
    }

    TileBase SelectTile()
    {
        int roll = Random.Range(1, 201); // Roll 1 to 100

        if (!keyObtained && !keySpawned)
        {
            float keyRockProbability = GetKeyRockChance(level); // e.g., 0.15 (15%)
            keyChance = Mathf.RoundToInt(keyRockProbability * 200f);
        } else
        {
            keyChance = 0;
        }

        int copperChance = keyChance + 20 + (1 * level);
        int ironChance = copperChance + 20 + (2 * (level - 10));
        int gemChance = ironChance + 2;

        if (roll <= keyChance)
        {
            keySpawned = true;
            return keyTile;
        }
         else if (roll <= copperChance) // 50% chance
        {
            return copperTile;
        }
        else if (roll <= ironChance) // 30% chance (51-80)
        {
            if (level > 10)
            {
                return ironTile;
            } else return rockTile;
        }
        else if (roll <= gemChance) // 15% chance (81-95)
        {
            if (level >= 5)
            {
                TileBase gem = SelectGem();
                return gem;
            }
            else return rockTile;
        }
        else // 5% chance (96-100)
        {
            if (level <= 10)
            {
                return rockTile;
            } else
            {
                return rockTile2;
            }
        }
    }

    TileBase SelectGem()
    {
        int choice = Random.Range(0, 2);
        if (choice == 0)
        {
            return amethystTile;
        } else
        {
            return rubyTile;
        }
    }

    const float A = 0.000225f;
    const float B = 1.4883f;

    public float GetKeyRockChance(int currentLevel)
    {
        float rawChance = A * Mathf.Pow(B, currentLevel);
        // Even at high levels, the chance is the probability of the single rock spawning
        return Mathf.Min(rawChance, 1.0f);
    }

    public void LadderCheck(Vector3Int targetCell, int rocksBroken)
    {
        if (!ladderSpawned)
        {
            int bonus = EnchantManager.Instance.enchantDic["Ladder"] ? 4 : 0;
            if (keyObtained) bonus += 2;
            int roll = Random.Range(1, 101);
            int chance = 2 + bonus + Mathf.RoundToInt((3.3f) * rocksBroken);

            if (roll <= chance)
            {
                interactionTilemap.SetTile(targetCell, ladderTile);
                ladderSpawned = true;
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Mine")
        {
            interactionTilemap = GameObject.FindGameObjectWithTag("Rocks Grid").GetComponent<Tilemap>();
            if (!keyObtained) keySpawned = false;
            ladderSpawned = false;
            level++;
            Debug.Log(level);
            SpawnRocks(initialRockCount);

            if (level == 21)
            {
                ladderSpawned = true;
                Tilemap gateTileMap = GameObject.FindGameObjectWithTag("GateTile").GetComponent<Tilemap>();
                gateTileMap.SetTile(new Vector3Int(-9, 0, 0), gateTile);
                interactionTilemap.SetTile(new Vector3Int(-9, 0, 0), gateTile);
            }
        }

        if (scene.name == "Base")
        {
            keyObtained = false;
            level = 0;
        }
    }
}