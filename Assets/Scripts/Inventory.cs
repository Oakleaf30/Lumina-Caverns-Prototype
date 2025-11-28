using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    // Use Dictionary to store all resources
    public Dictionary<string, int> resources = new Dictionary<string, int>()
    {
        {"Stone", 0},
        {"Copper", 0},
        {"Iron", 0},
        {"Amethyst", 0},
        {"Ruby", 0}
    };

    [System.Serializable]
    public class UpgradeEntry
    {
        public string name;
        public bool unlocked;
    }

    public Dictionary<string, bool> upgradeDic;

    public List<UpgradeEntry> upgrades = new List<UpgradeEntry>()
    {
        new UpgradeEntry { name = "Copper Pickaxe", unlocked = false },
        new UpgradeEntry { name = "Iron Pickaxe", unlocked = false },
        new UpgradeEntry { name = "Copper Sword", unlocked = false },
        new UpgradeEntry { name = "Iron Sword", unlocked = false },
        new UpgradeEntry { name = "Copper Armour", unlocked = false },
        new UpgradeEntry { name = "Iron Armour", unlocked = false },
    };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            upgradeDic = new Dictionary<string, bool>();
            foreach (var entry in upgrades)
            {
                upgradeDic[entry.name] = entry.unlocked;
            }
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddResource(string type, int amount = 1)
    {
        resources[type] += amount;
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
        if (scene.name == "Base")
        {
            DepositResources();
        }
    }

    private void DepositResources()
    {
        // 1. Deposit resources from inventory dictionary to Chest dictionary
        foreach (KeyValuePair<string, int> item in resources)
        {
            Chest.Instance.AddResource(item.Key, item.Value);
        }

        var keys = new List<string>(resources.Keys);
        foreach (string key in keys)
        {
            resources[key] = 0;
        }
    }

    public void ResourcesLost()
    {
        resources["Stone"] = 0;
        resources["Copper"] = 0;
        resources["Iron"] = 0;
    }

    public void UpdateUpgrades()
    {
        foreach (var entry in upgrades)
        {
            upgradeDic[entry.name] = entry.unlocked;
        }
    }
}
