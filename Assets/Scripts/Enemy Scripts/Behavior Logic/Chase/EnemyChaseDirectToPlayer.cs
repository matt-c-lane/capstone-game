using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _MovementSpeed = 1.75f; // Speed at which the enemy chases the player
    [SerializeField] private float chaseCooldown = 2f; // Time to keep chasing after player leaves aggro radius

    private float chaseTimer = 0f; // Timer to track how long the enemy has been chasing after losing aggro

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        chaseTimer = 0f; // Reset the chase timer when entering the chase state
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Check if the player is no longer aggroed
        if (!enemy.IsAggroed)
        {
            chaseTimer += Time.deltaTime; // Increment the chase timer

            // If the cooldown period has passed, transition back to IdleState
            if (chaseTimer >= chaseCooldown)
            {
                enemy.StateMachine.ChangeState(enemy.IdleState);
                chaseTimer = 0f; // Reset the timer
                return;
            }
        }
        else
        {
            chaseTimer = 0f; // Reset the timer if the player is still aggroed
        }

        // Move towards the player
        Vector3 movedDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(movedDirection * _MovementSpeed);
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