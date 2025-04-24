using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Dragon", menuName = "Enemy Logic/Chase Logic/Dragon Chase")]
public class DragonChase : EnemyChaseSOBase
{
    [SerializeField] private float _MovementSpeed = 3.5f;

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        enemy.Animator.SetBool("IsMoving", true);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.Animator.SetBool("IsMoving", false);
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(direction * _MovementSpeed);
    }
}
