using UnityEngine;
using System.Collections;
/*
RangedAttack is the base class for all ranged attacks. This inherits from Attack.
In general, you should not inherit directly from Attack.
Inherit from MeleeAttack or RangedAttack instead.
*/
[CreateAssetMenu(fileName = "New Ranged Attack", menuName = "Attacks/Ranged Attack")]
public class RangedAttack : Attack
{
    public float projectileRange = 5f;
    public float projectileSpeed = 10f;
    public override void Execute(Vector2 origin, Vector2 direction)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin + (direction * 1f), 0.5f, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(damage);
        }

        Player player = GameObject.FindAnyObjectByType<Player>();
        if (player != null && player.equippedWeapon is RangedWeapon rangedWeapon)
        {
            player.StartCoroutine(PlayAttackAnimation(player, rangedWeapon.weaponIcon, rangedWeapon.weaponSpriteSize));
            player.StartCoroutine(PlayProjectileAnimation(origin, direction, rangedWeapon));
        }
        
        // Register for debug drawing
        if (DebugManager.Instance != null)
        {
            DebugManager.Instance.RegisterRangedAttack(origin, direction, projectileRange);
        }
    }

    private IEnumerator PlayAttackAnimation(Player player, Sprite weaponSprite, Vector2 spriteSize)
    {
        GameObject weaponObj = new GameObject("Weapon Attack");
        SpriteRenderer renderer = weaponObj.AddComponent<SpriteRenderer>();
        renderer.sprite = weaponSprite;
        renderer.sortingOrder = 5;

        // Set sprite scale
        weaponObj.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);

        weaponObj.transform.position = player.transform.position;
        weaponObj.transform.parent = player.transform;

        yield return new WaitForSeconds(0.2f);
        GameObject.Destroy(weaponObj);
    }

    private IEnumerator PlayProjectileAnimation(Vector2 startPosition, Vector2 direction, RangedWeapon weapon)
    {
        GameObject projectileObj = new GameObject("Projectile");
        SpriteRenderer renderer = projectileObj.AddComponent<SpriteRenderer>();
        renderer.sprite = weapon.projectileSprite;
        renderer.sortingOrder = 6;

        // Set sprite scale
        projectileObj.transform.localScale = new Vector3(weapon.projectileSpriteSize.x, weapon.projectileSpriteSize.y, 1);

        projectileObj.transform.position = startPosition;

        float distanceTraveled = 0f;
        while (distanceTraveled < projectileRange)
        {
            float moveStep = projectileSpeed * Time.deltaTime;
            projectileObj.transform.position += (Vector3)(direction * moveStep);
            distanceTraveled += moveStep;
            yield return null;
        }

        GameObject.Destroy(projectileObj);
    }

    public void DrawDebug(Vector2 origin, Vector2 direction)
    {
        Gizmos.color = Color.blue; // Ranged attack area
        Vector2 endPosition = origin + (direction.normalized * projectileRange);
        Gizmos.DrawLine(origin, endPosition);
    }
}
