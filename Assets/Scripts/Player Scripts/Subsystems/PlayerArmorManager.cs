using UnityEngine;

public class PlayerArmorManager : PlayerManager
{
    // === Armor System ===
    public int armor = 1; //Protects against physical attacks, should never be zero
    public int shield = 1; //Protects against magical attacks, should never be zero
    public Wearable wearable; //Armor or clothes worn by player

    public PlayerArmorManager(Player player)
    {
        this.player = player;
    }
}