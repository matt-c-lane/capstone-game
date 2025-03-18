using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Attack-Straight-Single Projectile", menuName = "Enemy Logic/Attack Logic/Single Straight Projectile")]
public class NewMonoBehaviourScript : EnemyAttackSOBase
{
    [SerializeField] private Projectile fireballPrefab;
    [SerializeField] private float _timeBetweenShots = 2f;
    [SerializeField] private float _timeTillExit = 3f;
    [SerializeField] private float _distanceToCountExit = 3f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private int _bulletDamage = 10;
    [SerializeField] private DamageType _bulletType = DamageType.Magical;
    [SerializeField] private float _maxBulletDistance = 0f; // Optional for distance-based destruction

    private float _timer;
    private float _exitTimer;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;

            // Calculate direction towards the player
            Vector2 dir = (playerTransform.position - enemy.transform.position).normalized;

            // Instantiate the fireball
            Projectile bullet = Instantiate(fireballPrefab, enemy.transform.position, Quaternion.identity);
            bullet.Initialize(
                damage: _bulletDamage,
                damageType: _bulletType,
                speed: _bulletSpeed,
                direction: dir,
                maxDistance: _maxBulletDistance
            );
        }

        // Exit Logic
        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;
    }
    

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
