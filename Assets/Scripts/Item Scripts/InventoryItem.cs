using UnityEngine;
/*
InventoryItem represents an item managed by an Inventory. You should not inherit directly from this class.
Create a new ScriptableObject instead.
*/
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int quantity;
    public Sprite icon;

    public InventoryItem(string name, string desc, Sprite icn, int qty = 1)
    {
        itemName = name;
        description = desc;
        icon = icn;
        quantity = qty;
    }

    public override string ToString()
    {
        return $"{itemName} (x{quantity}): {description}";
    }
}
