using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resource : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private string CurrentSceneName => SceneManager.GetActiveScene().name;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (gameObject.name == "Key")
        {
            string obtained = RockSpawner.Instance.keyObtained ? "Yes" : "No";
            tmpText.text = "Key Obtained: " + obtained; 

        } else
        {
            if (CurrentSceneName == "Base")
            {
                tmpText.text = gameObject.name + ": " + Chest.Instance.GetCurrentAmount(gameObject.name);
            } else
            {
                tmpText.text = gameObject.name + ": " + Inventory.Instance.GetCurrentAmount(gameObject.name);
            }
        }
    }
}
