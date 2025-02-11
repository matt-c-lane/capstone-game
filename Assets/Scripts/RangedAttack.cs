using UnityEngine;

public class RangedAttack : Attack
{
    public float projectileRange = 5f;

    public override void Execute(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, projectileRange, enemyLayer);

        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Enemy>()?.TakeDamage(damage);
        }

        Debug.Log($"Ranged attack executed! Damage: {damage}");

        // Debug visualization
        Debug.DrawRay(origin, direction * projectileRange, Color.blue, 0.5f);
    }
}
