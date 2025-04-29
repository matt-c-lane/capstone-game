using UnityEngine;
using System.Collections;
/*
MeleeAttack is the base class for all melee attacks. This inherits from Attack.
In general, you should not inherit directly from Attack.
Inherit from MeleeAttack or RangedAttack instead.
*/
[CreateAssetMenu(fileName = "New Melee Attack", menuName = "Attacks/Melee Attack")]
public class MeleeAttack : Attack
{
    public float attackRadius = 1.5f;
    public float animationDuration = 0.2f;
    public float attackArcAngle = 90f; // Arc angle for the attack

    public override void Execute(Vector2 origin, Vector2 direction, int[] stats)
    {
        // Damage enemies in the 90-degree arc
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(origin, attackRadius, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 enemyDirection = (enemy.transform.position - (Vector3)origin).normalized;
            float angleToEnemy = Vector2.Angle(direction, enemyDirection);

            if (angleToEnemy <= attackArcAngle / 2f) 
            {
                //enemy.GetComponent<Enemy>()?.Damage(damage, damageType, stats);
            }
        }

        // Spawn the attack animation
        Player player = GameObject.FindAnyObjectByType<Player>();
        if (player != null && player.equippedWeapon is MeleeWeapon meleeWeapon)
        {
            player.StartCoroutine(PlayAttackAnimation(player, meleeWeapon.weaponIcon, meleeWeapon.spriteSize, meleeWeapon.attackOffset, direction));
        }

        if (DebugManager.Instance != null)
        {
            DebugManager.Instance.RegisterMeleeAttack(origin, attackRadius, attackArcAngle, direction);
        }

        // Debugging the arc
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -attackArcAngle / 2f) * direction * attackRadius;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, attackArcAngle / 2f) * direction * attackRadius;

        Debug.DrawRay(origin, leftBoundary, Color.red, 0.5f);
        Debug.DrawRay(origin, rightBoundary, Color.red, 0.5f);
    }

    private IEnumerator PlayAttackAnimation(Player player, Sprite attackSprite, Vector2 spriteSize, float offsetDistance, Vector2 direction)
    {
        GameObject swordObj = new GameObject("Sword Attack");
        SpriteRenderer renderer = swordObj.AddComponent<SpriteRenderer>();
        renderer.sprite = attackSprite;
        renderer.sortingLayerID = SortingLayer.NameToID("Player");
        renderer.sortingOrder = 5;

        swordObj.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);

        Vector2 origin = player.transform.position;

        float startAngle = -attackArcAngle / 2f;
        float endAngle = attackArcAngle / 2f;

        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            float worldAngle = baseAngle + angle;
            float radians = worldAngle * Mathf.Deg2Rad;

            Vector2 pos = origin + new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * offsetDistance;
            swordObj.transform.position = pos;

            swordObj.transform.rotation = Quaternion.Euler(0, 0, worldAngle);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GameObject.Destroy(swordObj);
    }

    public void DrawDebug(Vector2 origin, Vector2 direction)
    {
        Gizmos.color = Color.red;
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -attackArcAngle / 2f) * direction * attackRadius;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, attackArcAngle / 2f) * direction * attackRadius;

        Gizmos.DrawLine(origin, origin + leftBoundary);
        Gizmos.DrawLine(origin, origin + rightBoundary);
    }
}
