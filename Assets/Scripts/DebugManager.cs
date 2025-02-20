using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance { get; private set; }

    public bool showDebug = true; // Toggle debug visuals
    public GameObject debugCirclePrefab; // Assign a prefab for melee attack area
    private GameObject meleeDebugCircle;

    private LineRenderer lineRenderer; // For ranged attack visualization

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) // Press F1 to toggle
        {
            showDebug = !showDebug;
            lineRenderer.enabled = showDebug;
            if (meleeDebugCircle != null) meleeDebugCircle.SetActive(showDebug);
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
        }

        // Initialize LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    public void RegisterMeleeAttack(Vector2 origin, float radius)
    {
        if (!showDebug) return;

        // Destroy previous circle
        if (meleeDebugCircle != null) Destroy(meleeDebugCircle);

        // Create a new debug circle
        meleeDebugCircle = Instantiate(debugCirclePrefab, origin, Quaternion.identity);
        meleeDebugCircle.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
    }

    public void RegisterRangedAttack(Vector2 origin, Vector2 direction, float attackRange)
    {
        if (!showDebug) return;

        Vector2 endPosition = origin + (direction.normalized * attackRange);
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPosition);
    }
}
