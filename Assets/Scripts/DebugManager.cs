using UnityEngine;
using System.Collections.Generic;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance { get; private set; }

    public bool showDebug = true; // Toggle debug visuals
    public GameObject debugCirclePrefab; // Assign a prefab for melee attack area

    private List<(Vector2 origin, float radius)> meleeAttacks = new List<(Vector2, float)>();
    private List<(Vector2 origin, float radius, float arcAngle, Vector2 direction)> arcMeleeAttacks =
        new List<(Vector2, float, float, Vector2)>();

    private LineRenderer rangedLineRenderer; // For ranged attack visualization
    private LineRenderer arcLineRenderer;    // For melee arc visualization

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // Press F1 to toggle
        {
            showDebug = !showDebug;
            rangedLineRenderer.enabled = showDebug;
            arcLineRenderer.enabled = showDebug;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create GameObjects for each LineRenderer
        GameObject rangedLineObject = new GameObject("RangedLineRenderer");
        rangedLineObject.transform.SetParent(transform);

        GameObject arcLineObject = new GameObject("ArcLineRenderer");
        arcLineObject.transform.SetParent(transform);

        // Initialize ranged LineRenderer
        rangedLineRenderer = rangedLineObject.AddComponent<LineRenderer>();
        rangedLineRenderer.startWidth = 0.1f;
        rangedLineRenderer.endWidth = 0.1f;
        rangedLineRenderer.positionCount = 2;
        rangedLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        rangedLineRenderer.startColor = Color.blue;
        rangedLineRenderer.endColor = Color.blue;

        // Initialize arc LineRenderer
        arcLineRenderer = arcLineObject.AddComponent<LineRenderer>();
        arcLineRenderer.startWidth = 0.05f;
        arcLineRenderer.endWidth = 0.05f;
        arcLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        arcLineRenderer.startColor = Color.red;
        arcLineRenderer.endColor = Color.red;
        arcLineRenderer.positionCount = 0; // Start empty
    }

    public void RegisterMeleeAttack(Vector2 origin, float radius, float arcAngle, Vector2 direction)
    {
        arcMeleeAttacks.Add((origin, radius, arcAngle, direction.normalized));
        UpdateArcLineRenderer();
    }

    public void RegisterRangedAttack(Vector2 origin, Vector2 direction, float attackRange)
    {
        if (!showDebug) return;

        Vector2 endPosition = origin + (direction.normalized * attackRange);
        rangedLineRenderer.SetPosition(0, origin);
        rangedLineRenderer.SetPosition(1, endPosition);
    }

    private void UpdateArcLineRenderer()
    {
        if (!showDebug) return;

        List<Vector3> arcPoints = new List<Vector3>();

        foreach (var attack in arcMeleeAttacks)
        {
            Vector2 origin = attack.origin;
            float radius = attack.radius;
            float arcAngle = attack.arcAngle;
            Vector2 direction = attack.direction;

            // Draw the arc as points
            int arcResolution = 20; // Higher = smoother arc
            float halfArc = arcAngle / 2f;

            for (int i = 0; i <= arcResolution; i++)
            {
                float angle = Mathf.Lerp(-halfArc, halfArc, (float)i / arcResolution);
                Vector2 point = origin + (Vector2)(Quaternion.Euler(0, 0, angle) * (Vector3)direction * radius);
                arcPoints.Add(point);
            }
        }

        arcLineRenderer.positionCount = arcPoints.Count;
        arcLineRenderer.SetPositions(arcPoints.ToArray());
    }

    private void OnDrawGizmos()
    {
        if (!showDebug) return;

        Gizmos.color = Color.red;
        foreach (var attack in meleeAttacks)
        {
            Gizmos.DrawWireSphere(attack.origin, attack.radius);
        }

        foreach (var attack in arcMeleeAttacks)
        {
            Vector2 origin = attack.origin;
            float radius = attack.radius;
            float arcAngle = attack.arcAngle;
            Vector2 direction = attack.direction;

            Vector2 leftBoundary = Quaternion.Euler(0, 0, -arcAngle / 2f) * direction * radius;
            Vector2 rightBoundary = Quaternion.Euler(0, 0, arcAngle / 2f) * direction * radius;

            Gizmos.DrawLine(origin, origin + leftBoundary);
            Gizmos.DrawLine(origin, origin + rightBoundary);
        }
    }
}
