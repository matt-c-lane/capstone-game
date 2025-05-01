using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool IsFacingRight { get; set; } = true;

    public bool IsAggroed { get; set; }
    public bool IsWithinStrickingDistance { get; set; }

    public Animator Animator { get; private set; }

    public int armor = 1; //Physical attacks, should never be zero
    public int shield = 1; //Magic attacks, should never be zero

    public int exp = 1; //Amount of experience the player gets

    public void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackScript(this, StateMachine);
    }

    public void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.frameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Health/Damage Functions

    public void Damage(int damage, DamageType damageType, int[] stats)
    {
        float damageFactor = 1f; //Value attack damage is multiplied by
        float damageCalc; //Final attack damage before rounding

        if (damageType == DamageType.Physical)
        {
            damageFactor = (damage + stats[0])/armor;
        }
        else if (damageType == DamageType.Magical)
        {
            damageFactor = (damage + stats[1])/shield;
        }

        damageCalc = (damageFactor*damage);
        damage = (int)System.Math.Floor(damageCalc < 1 ? 1 : damageCalc); //All attacks deal at least 1 damage

        CurrentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage!");

        if (CurrentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        GiveExp();
        Destroy(gameObject);
    }
    private void GiveExp()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.leveler.GainExp(exp);
        Debug.Log($"Player gained {exp} EXP!");
    }
    
    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        RB.linearVelocity = velocity;
        CheckForLeftorRightFacing(velocity);
    }

    public void CheckForLeftorRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = false;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = true;
        }
    }

    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootStepSound,
        ScratchHit,
        FireBreath
    }

    #endregion

    #region StateMachine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyState IdleState { get; set; }
    public EnemyState ChaseState { get; set; }
    public EnemyState AttackState { get; set; }

    #endregion

    #region ScriptableObject Variables

    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    #region Distance Checks

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool value)
    {
        IsWithinStrickingDistance = value;
    }

    #endregion
}
