using UnityEngine;
public class Shopkeeper
{
    public string ShopkeeperName;
    public ShopItem[,] ShopInventory;
    //TODO: add scripts that manage the array
    public void Buyitem(ShopItem item){
    Player.wallet.Updategold(Shopitem.prc);
    //Runs payment and adds it into player inventory
    InventoryItem item= new InventoryItem(ShopItem.name, ShopItem.desc,ShopItem.icn,1);
    Player.Inventory.add(item);
    }
}