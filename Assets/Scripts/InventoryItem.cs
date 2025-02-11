using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public string description;
    public int quantity;

    public InventoryItem(string name, string desc, int qty = 1)
    {
        itemName = name;
        description = desc;
        quantity = qty;
    }

    public override string ToString()
    {
        return $"{itemName} (x{quantity}): {description}";
    }
}
