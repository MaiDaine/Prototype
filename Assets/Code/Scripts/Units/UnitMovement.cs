using UnityEngine;
using UnityEngine.AI;

namespace Prototype
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        private NavMeshAgent agent;

        public void Initialize(UnitStats unitStats)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = unitStats.moveSpeed;
            agent.stoppingDistance = unitStats.atkRange;
            agent.enabled = true;
        }

        public void StopMovement()
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            agent.updatePosition = false;
            //Idle Animation
        }

        public void ResumeMovement()
        {
            agent.isStopped = false;
            agent.updatePosition = true;
        }

        public void SetAgentDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            
            //agent.isStopped = false;
            //Move Animation
        }
    }
}