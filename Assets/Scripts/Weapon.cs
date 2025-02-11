using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float attackRange;

    public abstract void Attack(Vector2 attackDirection);
}
