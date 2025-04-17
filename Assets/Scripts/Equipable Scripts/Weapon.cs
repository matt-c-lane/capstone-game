using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public Sprite weaponIcon;
    public Attack attack;  // Reference to an Attack ScriptableObject
    public AudioClip effect;

    public float cooldownTime = 0.5f; // Time between attacks
    private float lastAttackTime = -Mathf.Infinity;

    public void ExecuteAttack(Vector2 origin, Vector2 direction, int[] stats)
    {
        if (Time.time < lastAttackTime + cooldownTime)
        {
            // Still in cooldown
            return;
        }

        if (attack != null)
        {
            attack.Execute(origin, direction, stats);
            lastAttackTime = Time.time;
        }
        else
        {
            Debug.LogWarning($"{weaponName} has no attack assigned!");
        }
    }
}
