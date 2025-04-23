using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class SlimePathfinding : MonoBehaviour
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
        agent.SetDestination(Player.position);
    }
}