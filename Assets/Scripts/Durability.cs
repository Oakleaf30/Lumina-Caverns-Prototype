using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Durability : MonoBehaviour
{
    public static Durability Instance;

    public TextMeshProUGUI durabilityText;

    public int maxDurability;
    public int durability;

    public enum Pickaxe
    {
        Stone,
        Copper,
        Iron
    }

    public Pickaxe currentPickaxe = Pickaxe.Stone;
    public int pickaxeDamage = 1;

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
        durability = maxDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePickaxe()
    {
        durability--;
        durabilityText.text = "Pickaxe Durability: " + durability;
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
            durabilityText = GameObject.FindGameObjectWithTag("DurabilityText").GetComponent<TextMeshProUGUI>();
        }
    }
}
