using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public void Initialize(Unit unit)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = unit.currentStats.moveSpeed;
        agent.stoppingDistance = unit.currentStats.atkRange;
        agent.enabled = true;
    }

    public void StopMovement()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        //Idle Animation
    }

    public void SetAgentDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.isStopped = false;
        //Move Animation
    }
}
