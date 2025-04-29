using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 5f;
    [SerializeField] private float RandomMovementSpeed = 1f;
    [SerializeField] private float idleDuration = 2f;

    private Vector3 _targetPos;
    private Vector3 _direction;
    private float _idleTimer;
    private bool _isWandering;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.Animator.SetBool("IsMoving", false); // Stop walking animation when exiting idle
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        if (_isWandering)
        {
            enemy.Animator.SetBool("IsMoving", true); // Play walking animation

            _direction = (_targetPos - enemy.transform.position).normalized;
            enemy.MoveEnemy(_direction * RandomMovementSpeed);

            if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.1f)
            {
                _isWandering = false;
                _idleTimer = 0f;
            }
        }
        else
        {
            enemy.Animator.SetBool("IsMoving", false); // Play idle animation

            enemy.MoveEnemy(Vector2.zero);
            _idleTimer += Time.deltaTime;

            if (_idleTimer >= idleDuration)
            {
                _isWandering = true;
                _targetPos = GetRandomPointInCircle();
            }
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        _isWandering = false;
        _idleTimer = 0f;
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        _isWandering = false;
        _idleTimer = 0f;

        enemy.Animator.SetBool("IsMoving", false); // Set to idle when entering state
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
}
