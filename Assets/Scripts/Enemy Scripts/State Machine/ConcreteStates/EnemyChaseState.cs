using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyChaseState : EnemyState
{
    private Transform _playerTransform;
    private float _MovementSpeed = 1.75f;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
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

        Vector3 movedDirection = (_playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(movedDirection * _MovementSpeed);

        if(enemy.IsWithinStrickingDistance)
        {
            enemyStateMachine.ChangeState(enemy.AttackState);
        
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
