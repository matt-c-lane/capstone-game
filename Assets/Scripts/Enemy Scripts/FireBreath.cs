/**using UnityEngine;

public class FireBreath : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 1.2f;
    [SerializeField] private ParticleSystem impactEffect;

    private float damage;
    private Vector2 direction;

    public void Initialize(Vector2 dir, float dmg)
    {
        direction = dir;
        damage = dmg;
        Destroy(gameObject, lifetime);

        // Flip sprite if moving left
        if (dir == Vector2.left)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(damage);
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
**/