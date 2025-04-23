using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class DragonPathfinding : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) > 1f)
            agent.SetDestination(Player.position);
    }
}