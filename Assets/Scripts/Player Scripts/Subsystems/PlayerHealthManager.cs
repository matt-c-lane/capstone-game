using UnityEngine;

public class PlayerHealthManager : PlayerManager
{
    // === Health System ===
    private int baseHealth;
    private int modHealth;
    public int maxHealth { get; private set; } = 10; //Player's max health
    public int currentHealth { get; private set; } //Player's current health

    public PlayerHealthManager(Player player)
    {
        this.player = player;
        currentHealth = maxHealth;
    }

    // === Health Functions ===
    public void IncreaseHealth(int amount) { currentHealth = Mathf.Min(currentHealth + amount, maxHealth); }
    public void DecreaseHealth(int amount) { currentHealth = Mathf.Max(currentHealth - amount, 0); if (currentHealth <= 0) player.Defeat(); }
    public void IncreaseMaxHealth(int amount) { maxHealth += amount; currentHealth += amount; }
    public void DecreaseMaxHealth(int amount) { maxHealth = Mathf.Max(maxHealth - amount, 1); currentHealth = Mathf.Min(currentHealth, maxHealth); }
    public void Damage(int damage, DamageType damageType)
    {
        float damageCalc = 1f;

        if (damageType == DamageType.Physical)
        {
            damageCalc = damage/player.armorer.armor;
        }
        else if (damageType == DamageType.Magical)
        {
            damageCalc = damage/player.armorer.shield;
        }
        else
        {
            Debug.Log($"Player recieved invalid DamageType: {damageType}");
        }

        damage = (int)System.Math.Floor(damageCalc < 1 ? 1 : damageCalc); //All attacks deal at least 1 damage
        DecreaseHealth(damage);
        player.uiManager.UpdateHealth(currentHealth, maxHealth);
    }
}