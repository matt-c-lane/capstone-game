using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyIdleSOBase : ScriptableObject
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
        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            
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



