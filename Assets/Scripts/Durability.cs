using TMPro;
using UnityEngine;

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

    public void DamagePickaxe()
    {
        durability--;
        durabilityText.text = "Pickaxe Durability: " + durability;
    }
}
