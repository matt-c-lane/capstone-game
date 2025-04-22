using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SlimePathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private float _updateInterval = 0.5f;
    [SerializeField] private float _chaseRadius = 8f;
    [SerializeField] private float _wanderRadius = 3f;
    [SerializeField] private LayerMask _groundLayer;

    private NavMeshAgent _agent;
    private Transform _player;
    private float _timer;
    private Vector3 _wanderTarget;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoRepath = true;
        _agent.speed = 1.5f; // Slower speed for slimes
        _agent.stoppingDistance = 0.5f;
    }

    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomWanderTarget();
    }

    void Update()
    {
        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(_player.position);
        }

        if (Time.time >= _timer)
        {
            UpdateMovement();
            _timer = Time.time + _updateInterval;
        }

       // Slime.SetDestination(_player.position);
        if (Time.time >= _timer)
        {
            UpdateMovement();
            _timer = Time.time + _updateInterval;
        }
    }

    void UpdateMovement()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        // Chase player if in range
        if (distanceToPlayer <= _chaseRadius)
        {
            if (_agent.isOnNavMesh)
            {
                _agent.SetDestination(_player.position);
            }
        }
        // Wander randomly if player is out of range
        else if (Vector3.Distance(transform.position, _wanderTarget) < 0.5f || !_agent.hasPath)
        {
            SetRandomWanderTarget();
        }
    }

    void SetRandomWanderTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _wanderRadius;
        Vector3 randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);
        _wanderTarget = transform.position + randomOffset;

        // Ensure target is on NavMesh
        if (NavMesh.SamplePosition(_wanderTarget, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas))
        {
            _wanderTarget = hit.position;
            _agent.SetDestination(_wanderTarget);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw chase radius (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);

        // Draw wander radius (blue)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _wanderRadius);

        // Draw current path (yellow)
        if (_agent != null && _agent.hasPath)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _agent.destination);
        }
    }
}