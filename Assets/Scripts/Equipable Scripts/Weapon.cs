using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public Sprite weaponIcon;
    public Attack attack;  // Reference to an Attack ScriptableObject
    public AudioClip effect;

    public void ExecuteAttack(Vector2 origin, Vector2 direction, int[] stats)
    {
        if (attack != null)
        {
            attack.Execute(origin, direction, stats);
        }
        else
        {
            Debug.LogWarning($"{weaponName} has no attack assigned!");
        }
    }
}