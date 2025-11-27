using System.Collections.Generic;
using UnityEngine;

public class EnchantManager : MonoBehaviour
{
    public static EnchantManager Instance;

    public float kb = 5;
    public float lightStrength = 5;
    public float moveSpeed = 5;

    public Dictionary<string, bool> enchantDic = new Dictionary<string, bool>()
    {
        {"Ores", false},
        {"Ladder", false},
        {"Knockback", false},
        {"Vampirism", false},
        {"Movement", false},
        {"Light", false},
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
}
