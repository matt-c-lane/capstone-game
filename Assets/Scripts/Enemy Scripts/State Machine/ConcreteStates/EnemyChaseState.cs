using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyChaseState : EnemyState
{
    
    public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
      
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.EnemyChaseBaseInstance.DoEnterlogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.EnemyChaseBaseInstance.DoExitLogic();
    }

    public override void frameUpdate()
    {
        base.frameUpdate();

        enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.EnemyChaseBaseInstance.DoPhysicsLogic();
    }
}
