using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public Sprite projectileSprite;
    public Vector2 weaponSpriteSize = new Vector2(1, 1); // Default size
    public Vector2 projectileSpriteSize = new Vector2(1, 1); // Default size
}
