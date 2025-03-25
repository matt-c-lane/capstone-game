using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifetime = 10f;

    protected int damage;
    protected DamageType damageType;
    protected float speed;
    protected float maxDistance;
    protected Vector2 direction;

    protected Vector2 _startPosition;
    protected float _timeAlive;

    public void Initialize(int damage, DamageType damageType, float speed, Vector2 direction, float maxDistance = 0f)
    {
        this.damage = damage;
        this.damageType = damageType;
        this.speed = speed;
        this.direction = direction.normalized;
        this.maxDistance = maxDistance;

        _startPosition = transform.position;
        _timeAlive = 0f;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        _timeAlive += Time.deltaTime;

        if ((maxDistance > 0f && Vector2.Distance(_startPosition, transform.position) >= maxDistance) ||
            _timeAlive >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.healther.Damage(damage, damageType);
                Destroy(gameObject);
            }
        }
    }
}