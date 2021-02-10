using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ShootingAI : MonoBehaviour
{
    [Header("Player info")]
    public Transform Player = null;
    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    public EnemyScripableObject EnemyOBJ;
    private float ChasePlayerRange;
    private float AttackRange;
    [Tooltip("The enemy's face, where they look")] [SerializeField] private Transform Face = null;
    private float Health = 0;
    [SerializeField]
    private float CurrentHealth = 0;

    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private Vector3 SpawnLocation;

    [Header("Shooter")]
    [Tooltip("Where the projectiles come from")] public Transform FirePoint = null;
    private float AttackCooldown = 0;
    private float BulletsPerShot;
    private float Bullets = 0;
    private float MaxSpread;

    // States
    private enum State {Initial, Idle, Patrol, Chase, Attack, Dead};
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
        gameObject.tag = "Shooter Enemy";

        //Player = PlayerRefs.instance.Player;

        Health = EnemyOBJ.Health;
        CurrentHealth = Health;

        animator = gameObject.GetComponent<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        SpawnLocation = transform.position;

        ChasePlayerRange = EnemyOBJ.ChaseRange;
        AttackRange = EnemyOBJ.AttackRange;

        BulletsPerShot = EnemyOBJ.BulletsPerShot;

        if(points.Length == 0)
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
        transform.LookAt(Player.position);
        if(AttackCooldown <= 0)
        {
            Shoot();
            AttackCooldown = 1 / EnemyOBJ.AttackRate;
        }
        AttackCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        while(Bullets < BulletsPerShot)
        {
            ObjectPooling.Spawn(EnemyOBJ.ProjectileOBJ.Projectile, FirePoint.position, FirePoint.rotation);
            Bullets += 1;
        }
        if(Bullets >= BulletsPerShot)
        {
            Bullets = 0;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.ChaseRange);
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.position);

        if(CurrentHealth <= 0)
        {
            ActiveState = State.Dead;
        }

        else if (PlayerDistance <= ChasePlayerRange && PlayerDistance > AttackRange)
        {
            transform.LookAt(Player);
            if (Physics.Raycast(Face.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    ActiveState = State.Chase;
                }
            }
        }
        else if (PlayerDistance <= AttackRange)
        {
            ActiveState = State.Attack;
        }

        else if (PlayerDistance > ChasePlayerRange && Idling)
        {
            if (transform.position != SpawnLocation)
            {
                Agent.destination = SpawnLocation;
            }
        }
    }
}
