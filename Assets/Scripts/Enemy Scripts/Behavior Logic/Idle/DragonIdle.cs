using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "DragonIdle", menuName = "Enemy Logic/Idle Logic/Dragon Idle")]
public class DragonIdle : EnemyIdleSOBase
{
    [Header("Flight Settings")]
    [SerializeField] private float hoverHeight = 3f;
    [SerializeField] private float hoverSpeed = 1f;
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private float circleSpeed = 0.5f;

    [Header("Behavior Settings")]
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 5f;

    private Vector3 homePosition;
    private float idleTimer;
    private float currentAngle;
    private float nextActionTime;
    private bool isCircling;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        homePosition = enemy.transform.position;
        SetNextActionTime();
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        homePosition = enemy.transform.position;
        idleTimer = 0f;
        currentAngle = 0f;
        isCircling = false;
        SetNextActionTime();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Check for player aggro
        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        idleTimer += Time.deltaTime;

        if (idleTimer >= nextActionTime)
        {
            isCircling = !isCircling;
            SetNextActionTime();
        }

        if (isCircling)
        {
            // Circular flying pattern
            currentAngle += circleSpeed * Time.deltaTime;
            Vector2 circleOffset = new Vector2(
                Mathf.Sin(currentAngle) * circleRadius,
                Mathf.Cos(currentAngle) * circleRadius
            );

            Vector3 targetPosition = homePosition +
                                   new Vector3(circleOffset.x, hoverHeight + circleOffset.y * 0.5f);

            Vector2 moveDirection = (targetPosition - enemy.transform.position).normalized;
            enemy.MoveEnemy(moveDirection * hoverSpeed);
        }
        else
        {
            // Simple hovering in place
            Vector3 targetPosition = homePosition + new Vector3(0, hoverHeight);
            Vector2 moveDirection = (targetPosition - enemy.transform.position).normalized;

            if (Vector2.Distance(enemy.transform.position, targetPosition) > 0.1f)
            {
                enemy.MoveEnemy(moveDirection * hoverSpeed);
            }
            else
            {
                enemy.MoveEnemy(Vector2.zero);
            }
        }
    }

    private void SetNextActionTime()
    {
        nextActionTime = Random.Range(minIdleTime, maxIdleTime);
        idleTimer = 0f;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.MoveEnemy(Vector2.zero);
    }

    public override void ResetValues()
    {
        base.ResetValues();
        idleTimer = 0f;
        currentAngle = 0f;
        isCircling = false;
    }
}