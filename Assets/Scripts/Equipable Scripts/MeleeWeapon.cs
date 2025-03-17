using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public Vector2 spriteSize = new Vector2(1, 1); // Default size (1x1)
    public float attackOffset = 1f;
}
