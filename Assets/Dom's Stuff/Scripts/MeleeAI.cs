﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MeleeAI : MonoBehaviour
{
    [Header("Player info")]
    public Transform Player = null;
    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    public EnemyScripableObject EnemyOBJ;
    private float ChasePlayerRange;
    private float AttackRange;
    private float Health = 0;
    [HideInInspector] public float CurrentHealth = 0;
    [Tooltip("Where the projectiles come from")] public Transform FirePoint = null;
    [SerializeField] private GameObject Weapon = null;


    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;
    private bool CanAttack = true; 

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private Vector3 SpawnLocation;

    // States
    private enum State { Initial, Idle, Patrol, Chase, Attack, Dead };
    private State ActiveState = State.Initial;
    private bool IsAlive = true;

    private IEnumerator Start()
    {
        while (IsAlive == true)
        {
            switch (ActiveState)
            {
                case State.Initial:
                    Initial();
                    break;
                case State.Idle:
                    Idle();
                    break;
                case State.Patrol:
                    Patrol();
                    break;
                case State.Chase:
                    Chase();
                    break;
                case State.Attack:
                    StartAttack();
                    break;
                case State.Dead:
                    DoDeath();
                    break;
            }
            yield return null;
        }
    }

    private void Initial()
    {
        gameObject.tag = "Melee Enemy";

        //Player = PlayerRefs.instance.Player;

        Health = EnemyOBJ.Health;
        CurrentHealth = Health;

        animator = gameObject.GetComponent<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        SpawnLocation = transform.position;

        ChasePlayerRange = EnemyOBJ.ChaseRange;
        AttackRange = EnemyOBJ.AttackRange;

        if (points.Length == 0)
        {
            ActiveState = State.Idle;
            Idling = true;
        }
        else
        {
            ActiveState = State.Patrol;
        }
    }

    private void Idle()
    {

    }

    private void Patrol()
    {
        if (points.Length == 0)
            return;
        Agent.destination = points[DestinationPoint].position;
        DestinationPoint = (DestinationPoint + 1) % points.Length;
    }

    private void Chase()
    {
        transform.LookAt(Player.position);
        Agent.destination = Player.position;
    }

    private void StartAttack()
    {
        Agent.isStopped = true;
        if (CanAttack)
        {
            CanAttack = false;
            animator.SetTrigger("Melee");
        }
        transform.LookAt(Player);
    }

    private void ExecuteAttack()
    {
        Weapon.GetComponent<Collider>().enabled = true;
    }

    private void StopAttack()
    {
        Weapon.GetComponent<Collider>().enabled = false;
        CanAttack = true;
        Agent.isStopped = false;
    }

    private void DoDeath()
    {
        Agent.isStopped = true;
        int RandomNumber = Random.Range(0, 100);
        if (RandomNumber <= EnemyOBJ.PickupOBJ.DropChance)
        {
            Instantiate(EnemyOBJ.PickupOBJ.PickupGameObject, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.position);

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            ActiveState = State.Dead;
        }

        if (PlayerDistance <= ChasePlayerRange && PlayerDistance > AttackRange)
        {
            ActiveState = State.Chase;
        }
        else if (PlayerDistance <= AttackRange)
        {
            ActiveState = State.Attack;
        }

        if (PlayerDistance > ChasePlayerRange && Idling)
        {
            if (transform.position != SpawnLocation)
            {
                Agent.destination = SpawnLocation;
                ActiveState = State.Idle;
            }
        }
        else if (PlayerDistance > ChasePlayerRange) 
        {
            ActiveState = State.Patrol;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.ChaseRange);
    }
}
