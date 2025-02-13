using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int quantity = 1; // Default value
    public Sprite icon;

    public override string ToString()
    {
        return $"{itemName} (x{quantity}): {description}";
    }

    // Factory method to create an item in runtime
    public static InventoryItem Create(string name, string desc, Sprite icn, int qty = 1)
    {
        InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
        newItem.itemName = name;
        newItem.description = desc;
        newItem.icon = icn;
        newItem.quantity = qty;
        return newItem;
    }
}