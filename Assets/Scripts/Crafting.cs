using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public string UpgradeType;
    public int UpgradeCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickaxeUpgrade()
    {
        if (Chest.Instance.storage[UpgradeType] >= UpgradeCost)
        {
            Chest.Instance.storage[UpgradeType] -= UpgradeCost;
            Durability.Instance.pickaxeDamage++;
            Durability.Instance.maxDurability += 30;
            Durability.Instance.durability = Durability.Instance.maxDurability;
            Button button = GetComponent<Button>();
            button.interactable = false;
        }
    }
}
