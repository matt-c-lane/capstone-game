using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem newItem)
    {
        // Check if item already exists in inventory
        InventoryItem existingItem = items.Find(item => item.itemName == newItem.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity; // Stack items
        }
        else
        {
            items.Add(newItem); // Add new item
        }
    }

    public void RemoveItem(string itemName, int amount = 1)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);
        if (item != null)
        {
            item.quantity -= amount;
            if (item.quantity <= 0)
            {
                items.Remove(item); // Remove if quantity reaches 0
            }
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Exists(item => item.itemName == itemName);
    }

    public void PrintInventory()
    {
        Debug.Log("Inventory:");
        foreach (var item in items)
        {
            Debug.Log(item.ToString());
        }
    }
}
