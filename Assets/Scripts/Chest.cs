using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest Instance;

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
