using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    public PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        tmpText.text = "Health: " + playerController.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay()
    {
        tmpText.text = "Health: " + playerController.currentHealth;
    }
}
