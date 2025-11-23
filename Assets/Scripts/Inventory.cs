using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int stone = 0;
    public int copper = 0;
    public int iron = 0;
    public int amethyst = 0;
    public int ruby = 0;

    // Use Dictionary to store all resources
    public Dictionary<string, int> resources = new Dictionary<string, int>()
    {
        {"Stone", 0},
        {"Copper", 0},
        {"Iron", 0},
        {"Amethyst", 0},
        {"Ruby", 0}
    };

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
}
