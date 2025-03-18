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
                enemy.GetComponent<Enemy>()?.Damage(damage, damageType, stats);
            }
        }

        // Spawn the attack animation
        Player player = GameObject.FindAnyObjectByType<Player>();
        if (player != null && player.equippedWeapon is MeleeWeapon meleeWeapon)
        {
            player.StartCoroutine(PlayAttackAnimation(player, meleeWeapon.weaponIcon, meleeWeapon.spriteSize, meleeWeapon.attackOffset));
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

    private IEnumerator PlayAttackAnimation(Player player, Sprite attackSprite, Vector2 spriteSize, float offsetDistance = 1f)
    {
        GameObject swordObj = new GameObject("Sword Attack");
        SpriteRenderer renderer = swordObj.AddComponent<SpriteRenderer>();
        renderer.sprite = attackSprite;
        renderer.sortingLayerID = SortingLayer.NameToID("Player");
        renderer.sortingOrder = 5;

        // Set sprite size
        swordObj.transform.localScale = new Vector3(spriteSize.x, spriteSize.y, 1);

        // Calculate initial position (offset from player in attack direction)
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector2 attackDir = (mousePosition - (Vector2)player.transform.position).normalized;
        Vector2 startPosition = (Vector2)player.transform.position + (attackDir * offsetDistance);

        // Place sword at initial position
        swordObj.transform.position = startPosition;

        // Rotate the sword around the player in a 90-degree arc
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float angle = Mathf.Lerp(-attackArcAngle / 2f, attackArcAngle / 2f, elapsedTime / animationDuration);
            float radians = angle * Mathf.Deg2Rad;

            Vector2 rotatedPosition = (Vector2)player.transform.position + new Vector2(
                Mathf.Cos(radians) * offsetDistance,
                Mathf.Sin(radians) * offsetDistance
            );

            swordObj.transform.position = rotatedPosition;
            swordObj.transform.rotation = Quaternion.Euler(0, 0, angle);

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
