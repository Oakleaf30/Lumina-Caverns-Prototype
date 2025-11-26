using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    public PlayerController playerController;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmpText.text = "Health: " + PlayerHealth.Instance.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDisplay()
    {
        tmpText.text = "Health: " + PlayerHealth.Instance.currentHealth;
    }
}
