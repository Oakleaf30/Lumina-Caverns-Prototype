using TMPro;
using UnityEngine;

public class DurabilityText : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        tmpText.text = "Pickaxe Durability: " + Durability.Instance.durability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
