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

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        breathTimer = 0f;
        scratchTimer = 0f;
        exitTimer = 0f;
        isBreathing = false;
        enemy.MoveEnemy(Vector2.zero);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.MoveEnemy(Vector2.zero);

        float distanceToPlayer = Vector2.Distance(playerTransform.position, enemy.transform.position);

        // Fire Breath Attack
        if (!isBreathing && breathTimer > timeBetweenBreaths && distanceToPlayer <= breathDistance)
        {
            breathTimer = 0f;
            isBreathing = true;

            if (animator != null)
            {
                animator.SetTrigger("FireBreath");
            }

            if (fireBreathPrefab != null)
            {
                Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
                GameObject fireBreathObj = Instantiate(fireBreathPrefab, enemy.transform.position, Quaternion.identity);
                currentBreath = fireBreathObj.GetComponent<Projectile>();

                if (currentBreath != null)
                {
                    currentBreath.Initialize(
                        damage: breathDamage,
                        damageType: DamageType.Magical,
                        speed: breathSpeed,
                        direction: direction,
                        maxDistance: 0f // Continuous breath
                    );
                }
                else
                {
                    Debug.LogError("Instantiated fire breath is missing Projectile component!", fireBreathObj);
                    Destroy(fireBreathObj);
                }
            }
            else
            {
                Debug.LogError("Fire breath prefab is not assigned in DragonAttack!", this);
                isBreathing = false;
            }
        }

        // Claw Attack
        if (scratchTimer > timeBetweenScratches && distanceToPlayer <= scratchRange)
        {
            scratchTimer = 0f;
            if (animator != null)
            {
                animator.SetTrigger("ScratchAttack");
            }
        }

        // Fire Breath Duration
        if (isBreathing)
        {
            breathTimer += Time.deltaTime;
            if (breathTimer >= breathDuration)
            {
                isBreathing = false;
                if (currentBreath != null)
                {
                    Destroy(currentBreath.gameObject);
                }
            }
        }
        else
        {
            breathTimer += Time.deltaTime;
        }

        scratchTimer += Time.deltaTime;

        // Exit Logic
        if (distanceToPlayer > distanceToCountExit)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer > timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            exitTimer = 0f;
        }
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == Enemy.AnimationTriggerType.ScratchHit)
        {
            if (Vector2.Distance(playerTransform.position, enemy.transform.position) <= scratchRange)
            {
                var damageable = playerTransform.GetComponent<IDamagable>();
                if (damageable != null)
                {
                    int[] stats = { 0, 0 }; // Default stats for physical attack
<<<<<<< Updated upstream
                    //damageable.Damage(scratchDamage, DamageType.Physical, stats);
=======
                    damageable.Damage(scratchDamage, DamageType.Physical, stats);
>>>>>>> Stashed changes
                }
            }
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        if (currentBreath != null)
        {
            Destroy(currentBreath.gameObject);
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