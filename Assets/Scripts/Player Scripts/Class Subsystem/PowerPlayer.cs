using UnityEngine;

public abstract class PowerPlayer : Power
{
    [HideInInspector] public Player player;
    public bool isActive = false; //Do not edit in inspector
    public bool onCooldown = false; //Do not edit in inspector
    // For some reason, isActive and onCooldown will set themselves to true, but only if they're private?
    // I have to leave them public, even though they really shouldn't be. Please be careful with this!

    public override void Activate() { /**Empty**/ }
    public abstract void Activate(Player player);
    public abstract void EndCooldown();
    public abstract void Restart();
}