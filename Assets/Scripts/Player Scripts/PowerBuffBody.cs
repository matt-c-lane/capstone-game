using UnityEngine;

[CreateAssetMenu(fileName = "New Body Buff", menuName = "Player/Buff Body Power")]
public class PowerBuffBody : PowerPlayer
{
    public int buff;
    public float duration = 5f;
    public float cooldown = 10f;
    // For some reason, isActive and onCooldown will set themselves to true, but only if they're private?
    // I have to leave them public, even though they really shouldn't be. Please be careful with this!

    void Awake()
    {
        isActive = false; 
        onCooldown = false;
    }

    void Start()
    {
        isActive = false; 
        onCooldown = false;
    }

    public override void Activate(Player player)
    {
        if (!isActive && !onCooldown)
        {
            this.player = player;
            isActive = true;
            ApplyBuff();
            player.StartCoroutine(player.ClassPowerDuration(duration));
        }
    }

    public override void Deactivate()
    {
        RemoveBuff();
        isActive = false;
        onCooldown = true;
        player.StartCoroutine(player.ClassPowerCooldown(cooldown));
    }

    public override void EndCooldown() { onCooldown = false; }
    public void ApplyBuff() { player.statser.ModBody(buff); }
    public void RemoveBuff() { player.statser.ModBody(-buff); }
}