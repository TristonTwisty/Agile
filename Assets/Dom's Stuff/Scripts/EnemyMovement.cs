using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [Tooltip("The areas the NPC will walk to")] public Transform[] PatrolPoints;
    private int DestinationPoint;
    [HideInInspector] public NavMeshAgent Agent;

    [HideInInspector] public bool AttackPlayer = false;
    [HideInInspector] public Transform Target;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        // Disabling auto braking stops AI from slowing down when it gets to a point
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
