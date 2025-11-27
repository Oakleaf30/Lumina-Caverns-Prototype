using UnityEngine;
using UnityEngine.UI;

public class Enchant : MonoBehaviour
{
    public string enchantName;
    public string enchantType;
    public int enchantCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        if (EnchantManager.Instance.enchantDic[enchantName])
        {
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Pickaxe Upgrades
    public void PickaxeAmethyst()
    {
        // Logic for applying Amethyst Pickaxe upgrade (e.g., increased resource yield)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
        }
    }

    public void PickaxeRuby()
    {
        // Logic for applying Ruby Pickaxe upgrade (e.g., max mining speed, special ability)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
        }
    }

    // Weapon Upgrades
    public void WeaponAmethyst()
    {
        // Logic for applying Amethyst Weapon upgrade (e.g., increased damage, critical chance)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
            EnchantManager.Instance.kb = 10;
        }
    }

    public void WeaponRuby()
    {
        // Logic for applying Ruby Weapon upgrade (e.g., heavy damage, life steal)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
        }
    }

    // Armour Upgrades
    public void ArmourAmethyst()
    {
        // Logic for applying Amethyst Armour upgrade (e.g., max health increase, small defense buff)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
            EnchantManager.Instance.moveSpeed = 7;
        }
    }

    public void ArmourRuby()
    {
        // Logic for applying Ruby Armour upgrade (e.g., high defense, unique passive effect)
        if (Chest.Instance.storage[enchantType] >= enchantCost)
        {
            EnchantBought();
            EnchantManager.Instance.lightStrength = 6;
        }
    }

    void EnchantBought()
    {
        EnchantManager.Instance.enchantDic[enchantName] = true;
        Button button = GetComponent<Button>();
        Chest.Instance.storage[enchantType] -= enchantCost;
        button.interactable = false;
    }
}
