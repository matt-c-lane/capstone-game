using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyChaseSOBase :ScriptableObject
{
    protected Enemy enemy;
    protected Transform tranfsorm;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        tranfsorm = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterlogic()
    {

    }
    public virtual void DoExitLogic()
    {
        ResetValues();
    }
    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.IsWithinStrickingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);

        }
    }
    public virtual void DoPhysicsLogic()
    {

    }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {

    }
    public virtual void ResetValues()
    {

    }
}

