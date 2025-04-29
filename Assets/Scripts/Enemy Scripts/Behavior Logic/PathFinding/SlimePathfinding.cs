using UnityEngine;
using UnityEngine.AI;

public class SlimePathfinding : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(Player.position);

        bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
        animator.SetBool("IsMoving", isMoving);
    }
}
