using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "Attack-Dragon", menuName = "Enemy Logic/Attack Logic/Dragon Attack")]
public class DragonAttack : EnemyAttackSOBase
{
    [SerializeField] private float closeRangeThreshold = 3f;

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();

        float distance = Vector2.Distance(enemy.transform.position, playerTransform.position);

        enemy.Animator.SetBool("IsAttacking", true);
        enemy.Animator.SetBool("IsMoving", false);

        if (distance <= closeRangeThreshold)
        {
            enemy.Animator.SetBool("IsClose", true);
            enemy.Animator.SetBool("IsFar", false);
        }
        else
        {
            enemy.Animator.SetBool("IsClose", false);
            enemy.Animator.SetBool("IsFar", true);
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();

        enemy.Animator.SetBool("IsAttacking", false);
        enemy.Animator.SetBool("IsClose", false);
        enemy.Animator.SetBool("IsFar", false);
        enemy.Animator.SetBool("IsMoving", true); // Resume walking
    }


}