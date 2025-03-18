using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
    public Attack attack;  // Reference to an Attack ScriptableObject

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