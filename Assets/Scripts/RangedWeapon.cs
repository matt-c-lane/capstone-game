using UnityEngine;

public class RangedWeapon : Weapon
{
    private RangedAttack rangedAttack;

    private void Awake()
    {
        rangedAttack = gameObject.AddComponent<RangedAttack>(); // Attach attack component
        rangedAttack.damage = damage;
        rangedAttack.projectileRange = attackRange;
        rangedAttack.enemyLayer = LayerMask.GetMask("Enemies");
    }

    public override void Attack(Vector2 attackDirection)
    {
        rangedAttack.Execute(transform.position, attackDirection);
    }
}
