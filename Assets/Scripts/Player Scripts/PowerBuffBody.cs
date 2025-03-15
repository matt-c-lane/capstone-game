using UnityEngine;

[CreateAssetMenu(fileName = "New Body Buff", menuName = "Player/Buff Body Power")]
public class PowerBuffBody : PowerPlayer
{
    public int buff;
    public float duration = 5f;
    public float cooldown = 10f;
    private bool isActive = false;
    private bool onCooldown = false;

    public override void Activate(Player player)
    {
        if (!isActive)
        {
            this.player = player;
            isActive = true;
            ApplyEffect();
            player.StartCoroutine(BuffDuration());
        }
    }

    private IEnumerator BuffDuration()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    private IEnumerator ActivateCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    public override void Deactivate() { RemoveBuff(); isActive = false; onCooldown = true; }
    public void ApplyBuff() { player.ModBody(buff); }
    public void RemoveBuff() { player.ModBody(-buff); }
}