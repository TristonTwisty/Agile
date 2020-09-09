using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    public Transform[] PatrolPoints;
    private int DestinationPoint;
    private NavMeshAgent Agent;

    [HideInInspector] public bool AttackPlayer = false;
    [HideInInspector] public Transform Target;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;

        DoPatrol();
    }

    private void Update()
    {
        if(!AttackPlayer)
        {
            if(!Agent.pathPending && Agent.remainingDistance < .5f)
            {
                DoPatrol();
            }
        }
        else
        {
            Agent.destination = Target.position;
        }
    }

    private void DoPatrol()
    {
        if (PatrolPoints.Length == 0)
            return;

        Agent.destination = PatrolPoints[DestinationPoint].position;
        DestinationPoint = (DestinationPoint + 1) % PatrolPoints.Length;
    }
}
