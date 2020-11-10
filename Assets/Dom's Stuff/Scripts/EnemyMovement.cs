using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public EnemyScripableObject EnemyOBJ = null;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public bool AttackPlayer = false;
    [HideInInspector] public Transform Target;
    private float TargetDistance;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();

        Agent.speed = EnemyOBJ.MovementSpeed;

        // Disabling auto braking stops AI from slowing down when it gets to a point
        Agent.autoBraking = false;
    }

    private void Update()
    {
        TargetDistance = Vector3.Distance(Target.position, transform.position);
        Agent.destination = Target.position;

        if(TargetDistance <= EnemyOBJ.AttackRange)
        {
            Agent.isStopped = true;
        }
        else
        {
            Agent.isStopped = false;
        }
    }
}
