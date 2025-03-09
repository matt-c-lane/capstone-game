using UnityEngine;
using System.Collections;
/*
RangedAttack is the base class for all ranged attacks. This inherits from Attack.
In general, you should not inherit directly from Attack.
Inherit from MeleeAttack or RangedAttack instead.
*/
public enum AttackMode
{
    Hitscan,
    Projectile
}

[CreateAssetMenu(fileName = "New Ranged Attack", menuName = "Attacks/Ranged Attack")]
public class RangedAttack : Attack
{
    public AttackMode attackMode = AttackMode.Hitscan;
    public float projectileRange = 5f;
    public float projectileSpeed = 10f;
    public PlayerProjectile projectilePrefab;

    public override void Execute(Vector2 origin, Vector2 direction, int[] stats)
    {
        if (attackMode == AttackMode.Hitscan)
        {
            PerformHitscanAttack(origin, direction, stats);
        }
        else if (attackMode == AttackMode.Projectile)
        {
            PerformProjectileAttack(origin, direction, stats);
        }

        Player player = GameObject.FindAnyObjectByType<Player>();
        if (player != null && player.equippedWeapon is RangedWeapon rangedWeapon)
        {
            player.StartCoroutine(PlayAttackAnimation(player, rangedWeapon.weaponIcon, rangedWeapon.weaponSpriteSize));
        }

        if (DebugManager.Instance != null)
        {
            DebugManager.Instance.RegisterRangedAttack(origin, direction, projectileRange);
        }
    }

    private void PerformHitscanAttack(Vector2 origin, Vector2 direction, int[] stats)
    {
        RaycastHit2D[] hitEnemies = Physics2D.RaycastAll(origin, direction, projectileRange, enemyLayer);
        foreach (RaycastHit2D target in hitEnemies)
        {
            Enemy enemy = target.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage, damageType, stats);
            }
        }
    }

    private void PerformProjectileAttack(Vector2 origin, Vector2 direction, int[] stats)
    {
        if (projectilePrefab != null)
        {
            PlayerProjectile projectile = Instantiate(projectilePrefab, origin, Quaternion.identity);
            projectile.Initialize(
                damage: damage,
                speed: projectileSpeed,
                direction: direction.normalized,
                damageType: damageType,
                stats: stats,
                maxDistance: projectileRange
            );
        }
        else
        {
            Debug.LogWarning("Projectile prefab is missing!");
        }
    }

    private IEnumerator PlayAttackAnimation(Player player, Sprite weaponSprite, Vector2 spriteSize)
    {
        GameObject weaponObj = new GameObject("Weapon Attack");
        SpriteRenderer renderer = weaponObj.AddComponent<SpriteRenderer>();
        renderer.sprite = weaponSprite;
        renderer.sortingLayerID = SortingLayer.NameToID("Player");
        renderer.sortingOrder = 5;

        weaponObj.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);
        weaponObj.transform.position = player.transform.position;
        weaponObj.transform.parent = player.transform;

        yield return new WaitForSeconds(0.2f);
        GameObject.Destroy(weaponObj);
    }

    public void DrawDebug(Vector2 origin, Vector2 direction)
    {
        Gizmos.color = Color.blue;
        Vector2 endPosition = origin + (direction.normalized * projectileRange);
        Gizmos.DrawLine(origin, endPosition);
    }
}
