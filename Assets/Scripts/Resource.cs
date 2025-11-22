using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

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
            tmpText.text = gameObject.name + ": " + Inventory.Instance.GetCurrentAmount(gameObject.name);
        }
    }
}
