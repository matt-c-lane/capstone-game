using UnityEngine;

[CreateAssetMenu(fileName = "DragonChase", menuName = "Enemy Logic/Chase Logic/Dragon Chase 2D")]
public class DragonChase : EnemyChaseSOBase
{
    [Header("Flight Settings")]
    [SerializeField] private float flyingSpeed = 5f;
    [SerializeField] private float minHeightAbovePlayer = 1.5f;
    [SerializeField] private float maxHeightAbovePlayer = 3f;
    [SerializeField] private float heightAdjustSpeed = 2f;
    [SerializeField] private float chaseCooldown = 3f;

    [Header("Hovering Settings")]
    [SerializeField] private float horizontalHoverDistance = 2f;
    [SerializeField] private float hoverSpeed = 1f;

    private float currentHeight;
    private float chaseTimer;
    private Vector2 hoverOffset;
    private float hoverTimer;

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        currentHeight = minHeightAbovePlayer;
        GenerateNewHoverOffset();
    }

    public override void DoEnterlogic()
    {
        base.DoEnterlogic();
        chaseTimer = 0f;
        hoverTimer = 0f;
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // Transition to attack if in range
        if (enemy.IsWithinStrickingDistance)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
            return;
        }

        // Handle chase cooldown
        if (!enemy.IsAggroed)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= chaseCooldown)
            {
                enemy.StateMachine.ChangeState(enemy.IdleState);
                return;
            }
        }
        else
        {
            chaseTimer = 0f;
        }

        // Calculate target position
        Vector2 playerPosition = playerTransform.position;

        // Smooth height adjustment
        float targetHeight = Mathf.Lerp(minHeightAbovePlayer, maxHeightAbovePlayer, 0.5f);
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, heightAdjustSpeed * Time.deltaTime);

        // Update hover pattern
        hoverTimer += Time.deltaTime * hoverSpeed;
        if (hoverTimer > Mathf.PI * 2) // Complete hover cycle
        {
            hoverTimer = 0f;
            GenerateNewHoverOffset();
        }

        // Calculate hover position
        Vector2 hoverPosition = new Vector2(
            Mathf.Sin(hoverTimer) * horizontalHoverDistance,
            0
        );

        // Final target position (player position + height + hover)
        Vector2 targetPosition = playerPosition +
                               new Vector2(0, currentHeight) +
                               hoverOffset + hoverPosition;

        // Movement direction
        Vector2 moveDirection = (targetPosition - (Vector2)enemy.transform.position).normalized;
        enemy.MoveEnemy(moveDirection * flyingSpeed);
    }

    private void GenerateNewHoverOffset()
    {
        hoverOffset = new Vector2(
            Random.Range(-horizontalHoverDistance, horizontalHoverDistance),
            Random.Range(0, currentHeight * 0.3f)
        );
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        ResetValues();
    }

    public override void ResetValues()
    {
        base.ResetValues();
        currentHeight = minHeightAbovePlayer;
        chaseTimer = 0f;
        hoverTimer = 0f;
    }
}