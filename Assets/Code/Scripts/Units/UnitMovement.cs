﻿using UnityEngine;
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
            agent.acceleration = unitStats.moveSpeed * 2f;
            agent.stoppingDistance = unitStats.atkRange;
            agent.enabled = true;
        }

        public void OnSpeedChange(float moveSpeed)
        {
            agent.speed = moveSpeed;
            agent.acceleration = moveSpeed * 2f;
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
            agent.Warp(gameObject.transform.position);
            agent.isStopped = false;
            agent.updatePosition = true;
        }

        public void SetAgentDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            //TMP
            agent.isStopped = false;
            agent.updatePosition = true;

            //Move Animation
        }
    }
}