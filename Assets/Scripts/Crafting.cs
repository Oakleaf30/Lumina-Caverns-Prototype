using System;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public string UpgradeType;
    public int UpgradeCost;
    public string UpgradeName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        if (Inventory.Instance.upgradeDic[UpgradeName])
        {
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeBought()
    {
        Button button = GetComponent<Button>();
        Chest.Instance.storage[UpgradeType] -= UpgradeCost;
        button.interactable = false;
        Inventory.Instance.upgradeDic[UpgradeName] = true;
    }

    public void PickaxeUpgrade()
    {
        if (Chest.Instance.storage[UpgradeType] >= UpgradeCost)
        {
            UpgradeBought();
            Durability.Instance.pickaxeDamage++;
            Durability.Instance.maxDurability += 60;
            Durability.Instance.durability = Durability.Instance.maxDurability;
            Durability.Pickaxe newType = (Durability.Pickaxe)Enum.Parse(
            typeof(Durability.Pickaxe),
            UpgradeType,
            true // Ignore case
        );
            Durability.Instance.currentPickaxe = newType;
        }
    }

    public void WeaponUpgrade()
    {
        if (Chest.Instance.storage[UpgradeType] >= UpgradeCost)
        {
            UpgradeBought();
            PlayerHealth.Instance.attackDamage += 5; 
        }
    }

    public void ArmourUpgrade()
    {
        if (Chest.Instance.storage[UpgradeType] >= UpgradeCost)
        {
            UpgradeBought();
            PlayerHealth.Instance.maxHealth += 25;
            PlayerHealth.Instance.currentHealth = PlayerHealth.Instance.maxHealth; 
        }
    }
}
