using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttackScript : EnemyState
{

    private Transform _playerTransform;

    private float _timer;
    private float _timeBetweenShots = 2f;

    private float _exitTimer;
    private float _timeTillExit =3f;
    private float _distanceToCountExit = 3f;

    private float _bulletSpeed = 10f;
    public EnemyAttackScript(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void frameUpdate()
    {
        base.frameUpdate();

        enemy.MoveEnemy(Vector2.zero);

        if(_timer> _timeBetweenShots)
        {
            _timer = 0f;
            Vector2 dir = (_playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(enemy.BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.linearVelocity = dir * _bulletSpeed;
        }
        if (Vector2.Distance(_playerTransform.position, enemy.transform.position) > _distanceToCountExit) 
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _timeTillExit)
            {
                enemyStateMachine.ChangeState(enemy.ChaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }
            _timer += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
