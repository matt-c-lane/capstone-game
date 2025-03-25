using UnityEngine;

public class PlayerManaManager : PlayerManager
{
    // === Magic System ===
    private int baseMana;
    private int modMana;
    public int maxMana { get; private set; } = 10; //Player's max mana, used by spells
    public int currentMana { get; private set; }

    public PlayerManaManager(Player player)
    {
        this.player = player;
        currentMana = maxMana;
    }
}