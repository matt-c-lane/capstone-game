using UnityEngine;

public class PlayerLevelManager : PlayerManager
{
    // === Progression System ===
    public int lvl { get; private set; } = 1; //Player's level
    private int exp = 0; //Player's XP
    private int expNextLvl = 100; //Amount of XP the player needs to reach the next level
    private int expNextMod = 10; //Amount expNextLvl increases by every time the player levels up
    public int levelBuffer = 0; //Amount of levels the player has gained with multiple level ups

    public PlayerLevelManager(Player player)
    {
        this.player = player;
    }

    // === Progression Fuctions ===
    private void IncreaseLvl(int amount) { levelBuffer += amount; CalcExpNext(); }
    private void CalcExpNext() { expNextLvl += expNextMod; }
    private void GainLvl() { exp -= expNextLvl; IncreaseLvl(1); }
    private void SpendLevel() { levelBuffer--; lvl++; }

    public void GainExp(int amount)
    {
        exp += amount;
        while(exp >= expNextLvl) GainLvl();
        player.uiManager.UpdateExp(exp, expNextLvl);
    }
}