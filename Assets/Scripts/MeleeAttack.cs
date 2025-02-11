using UnityEngine;

public class MeleeAttack : Attack
{
    public float attackRadius = 1.5f;

    public override void Execute(Vector2 origin, Vector2 direction)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin, attackRadius, enemyLayer);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(damage);
        }

        Debug.Log($"Melee attack executed! Damage: {damage}");

        // Debug visualization
        Debug.DrawRay(origin, direction * attackRadius, Color.red, 0.5f);
    }
}
