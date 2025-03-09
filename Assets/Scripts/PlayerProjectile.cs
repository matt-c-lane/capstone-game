using UnityEngine;

public class PlayerProjectile : Projectile
{
    protected string damageType;
    protected int[] stats;

    public void Initialize(int damage, float speed, Vector2 direction, string damageType, int[] stats, float maxDistance = 0f)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction.normalized;
        this.maxDistance = maxDistance;
        this.damageType = damageType;
        this.stats = stats;

        _startPosition = transform.position;
        _timeAlive = 0f;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage, damageType, stats);
                Destroy(gameObject);
            }
        }
    }
}