using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class DragonPathfinding : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] private float _updateInterval = 0.3f;
    [SerializeField] private LayerMask _walkableLayers;
    [SerializeField] private float _scanRadius = 10f;

    private NavMeshAgent _agent;
    private Transform _player;
    private float _timer;
    private NavMeshData _navMeshData;
    private NavMeshDataInstance _navMeshInstance;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoRepath = true;
        _navMeshData = new NavMeshData();
        _navMeshInstance = NavMesh.AddNavMeshData(_navMeshData);
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        BakeLocalNavMesh();
    }

    void Update()
    {
        if (Time.time >= _timer && _player != null)
        {
            UpdatePath();
            _timer = Time.time + _updateInterval;
        }
    }

    void UpdatePath()
    {
        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(_player.position);
        }
    }

    void BakeLocalNavMesh()
    {
        NavMeshBuildSettings settings = NavMesh.GetSettingsByID(0);
        Bounds bounds = new Bounds(transform.position, Vector3.one * _scanRadius);

        List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();
        NavMeshBuilder.CollectSources(
            bounds,
            _walkableLayers,
            NavMeshCollectGeometry.PhysicsColliders,
            0,
            new List<NavMeshBuildMarkup>(),
            sources
        );

        NavMeshBuilder.UpdateNavMeshData(
            _navMeshData,
            settings,
            sources,
            bounds
        );
    }

    void OnDestroy()
    {
        NavMesh.RemoveNavMeshData(_navMeshInstance);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
#endif
}