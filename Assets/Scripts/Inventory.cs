using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int copper = 0;
    public int iron = 0;
    public int amethyst = 0;
    public int ruby = 0;

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
        // Use a switch statement to check the type string
        switch (type)
        {
            case "Copper":
                copper += amount;
                break;
            case "Iron":
                iron += amount;
                break;
            case "Amethyst":
                amethyst += amount;
                break;
            case "Ruby":
                ruby += amount;
                break;
        }
    }

    public int GetCurrentAmount(string type)
    {
        switch (type)
        {
            case "Copper": return copper;
            case "Iron": return iron;
            case "Amethyst": return amethyst;
            case "Ruby": return ruby;
            default: return 0;
        }
    }
}
