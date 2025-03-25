using UnityEngine;
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int quantity;
    public int price;
    public Sprite icon;

    public ShopItem(string name, string desc, Sprite icn, int prc = 1)
    {
        itemName = name;
        description = desc;
        icon = icn;
        price = prc;
    }

    public override string ToString()
    {
        return $"{itemName} (x{quantity}): {description}";
    }
}
