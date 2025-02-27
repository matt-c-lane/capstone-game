using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttackScript : EnemyState
{
    public EnemyAttackScript(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemy.EnemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyAttackBaseInstance.DoEnterlogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyAttackBaseInstance.DoExitLogic();
    }

    public override void frameUpdate()
    {
        base.frameUpdate();
        enemy.EnemyAttackBaseInstance.DoFrameUpdateLogic();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyAttackBaseInstance.DoPhysicsLogic();
    }
}
