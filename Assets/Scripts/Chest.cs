using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest Instance;

    public Dictionary<string, int> storage = new Dictionary<string, int>()
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

    public void AddResource(string type, int amount)
    {
        storage[type] += amount;
    }
}
