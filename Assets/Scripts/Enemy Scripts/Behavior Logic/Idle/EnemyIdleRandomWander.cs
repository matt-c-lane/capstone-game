using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 5f; // Range for random wandering
    [SerializeField] private float RandomMovementSpeed = 1f; // Speed for wandering
    [SerializeField] private float idleDuration = 2f; // Time to stay idle before wandering

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
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Check if the player is aggroed
        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        // Idle and wander logic
        if (_isWandering)
        {
            // Move towards the target position
            _direction = (_targetPos - enemy.transform.position).normalized;
            enemy.MoveEnemy(_direction * RandomMovementSpeed);

            // If close to the target, stop wandering and go idle
            if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.1f)
            {
                _isWandering = false;
                _idleTimer = 0f; // Reset idle timer
            }
        }
        else
        {
            // Idle behavior
            enemy.MoveEnemy(Vector2.zero); // Stop moving

            // Increment idle timer
            _idleTimer += Time.deltaTime;

            // If idle duration is over, start wandering
            if (_idleTimer >= idleDuration)
            {
                _isWandering = true;
                _targetPos = GetRandomPointInCircle(); // Set a new wander target
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
        _isWandering = false; // Start in idle state
        _idleTimer = 0f; // Reset idle timer
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        _isWandering = false; // Start in idle state
        _idleTimer = 0f; // Reset idle timer
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