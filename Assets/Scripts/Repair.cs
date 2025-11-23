using UnityEngine;

public class Repair : MonoBehaviour
{
    public string RepairType;
    public int RepairCost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RepairPickaxe()
    {
        if (Durability.Instance.currentPickaxe.ToString() == RepairType && Chest.Instance.storage[RepairType] >= RepairCost)
        {
            Chest.Instance.storage[RepairType] -= RepairCost;
            Durability.Instance.durability = Durability.Instance.maxDurability;
        }
    }
}
