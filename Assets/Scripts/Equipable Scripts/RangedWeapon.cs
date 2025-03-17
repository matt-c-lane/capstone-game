using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public Sprite projectileSprite;
    public Vector2 weaponSpriteSize = new Vector2(1, 1);
    public Vector2 projectileSpriteSize = new Vector2(1, 1);
}
