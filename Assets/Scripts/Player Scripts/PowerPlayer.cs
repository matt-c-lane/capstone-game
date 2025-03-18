using UnityEngine;

public abstract class PowerPlayer : Power
{
    public Player player;

    public override void Activate() { /**Empty**/ }
    public abstract void Activate(Player player);
    public abstract void EndCooldown();
}