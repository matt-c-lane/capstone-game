/**

using UnityEngine;

public class Fireball : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(damage);
                Destroy(gameObject); // Destroy the fireball upon hitting the player
            }
        }
    }
}

**/