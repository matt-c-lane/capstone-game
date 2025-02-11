using UnityEngine;

public class MeleeWeapon : Weapon
{
    private MeleeAttack meleeAttack;

    private void Awake()
    {
        meleeAttack = gameObject.AddComponent<MeleeAttack>(); // Attach attack component
        meleeAttack.damage = damage;
        meleeAttack.attackRadius = attackRange;
        meleeAttack.enemyLayer = LayerMask.GetMask("Enemies");
    }

    public override void Attack(Vector2 attackDirection)
    {
        meleeAttack.Execute(transform.position, attackDirection);
    }
}
