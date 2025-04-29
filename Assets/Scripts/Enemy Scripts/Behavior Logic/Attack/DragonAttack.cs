using UnityEngine;
using System.Collections.Generic;
using System.Collections;
[CreateAssetMenu(fileName = "DragonAttack", menuName = "Enemy Logic/Attack Logic/Dragon Attack")]
public class DragonAttack : EnemyAttackSOBase
{
    [Header("Fire Breath Settings")]
    [SerializeField] private GameObject fireBreathPrefab; // Changed to GameObject type
    [SerializeField] private float timeBetweenBreaths = 3f;
    [SerializeField] private float breathDuration = 2f;
    [SerializeField] private float breathDistance = 5f;
    [SerializeField] private float breathSpeed = 8f;
    [SerializeField] private int breathDamage = 5;

    [Header("Claw Attack Settings")]
    [SerializeField] private float scratchRange = 2f;
    [SerializeField] private int scratchDamage = 15;
    [SerializeField] private float timeBetweenScratches = 1.5f;

    [Header("Common Settings")]
    [SerializeField] private float timeTillExit = 3f;
    [SerializeField] private float distanceToCountExit = 6f;

    private float breathTimer;
    private float scratchTimer;
    private float exitTimer;
    private bool isBreathing = false;
    private Projectile currentBreath;
    private Animator animator;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        animator = gameObject.GetComponent<Animator>();

#if UNITY_EDITOR
        if (fireBreathPrefab != null && fireBreathPrefab.GetComponent<Projectile>() == null)
        {
            Debug.LogError($"Fire breath prefab is missing Projectile component!", fireBreathPrefab);
        }
#endif
    }
    [SerializeField] private float closeRangeThreshold = 3f;

    using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Dragon", menuName = "Enemy Logic/Attack Logic/Dragon Attack")]
public class DragonAttack : EnemyAttackSOBase
{
    [SerializeField] private float closeRangeThreshold = 3f;

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();

        float distance = Vector2.Distance(enemy.transform.position, playerTransform.position);

        enemy.Animator.SetBool("IsAttacking", true);
        enemy.Animator.SetBool("IsMoving", false);

        if (distance <= closeRangeThreshold)
        {
            enemy.Animator.SetBool("IsClose", true);
            enemy.Animator.SetBool("IsFar", false);
        }
        else
        {
            enemy.Animator.SetBool("IsClose", false);
            enemy.Animator.SetBool("IsFar", true);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        enemy.Animator.SetBool("IsAttacking", false);
        enemy.Animator.SetBool("IsClose", false);
        enemy.Animator.SetBool("IsFar", false);
        enemy.Animator.SetBool("IsMoving", true); // Resume walking
    }
}



    public override void ResetValues()
    {
        base.ResetValues();
        breathTimer = 0f;
        scratchTimer = 0f;
        exitTimer = 0f;
        isBreathing = false;
        currentBreath = null;
    }
}