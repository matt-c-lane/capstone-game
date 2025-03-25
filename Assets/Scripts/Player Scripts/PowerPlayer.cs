using UnityEngine;

public abstract class PowerPlayer : Power
{
    [HideInInspector] public Player player;
    public bool isActive = false; //Do not edit in inspector
    public bool onCooldown = false; //Do not edit in inspector

    public override void Activate() { /**Empty**/ }
    public abstract void Activate(Player player);
    public abstract void EndCooldown();
}