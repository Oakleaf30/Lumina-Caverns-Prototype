using UnityEngine;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Inventory.Instance.tutorial)
        {
            HidePopup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidePopup()
    {
        gameObject.SetActive(false);
        Inventory.Instance.tutorial = true;
    }
}
