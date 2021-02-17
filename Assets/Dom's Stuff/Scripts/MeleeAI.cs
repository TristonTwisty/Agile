using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MeleeAI : MonoBehaviour
{
    [Header("Player info")]
    private Transform Player;
    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    public EnemyScripableObject EnemyOBJ;
    private float ChasePlayerRange;
    private float AttackRange;
    private float Health = 0;
    [HideInInspector] public float CurrentHealth = 0;
    [Tooltip("The enemy's face, where they look")] [SerializeField] private Transform Face = null;


    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;
    private bool CanAttack = true; 

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private Vector3 SpawnLocation;

    [Header("Melee")]
    [SerializeField] private GameObject Weapon = null;

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
        Player = PlayerRefs.instance.Player;

        gameObject.tag = "Melee Enemy";

        Health = EnemyOBJ.Health;
        CurrentHealth = Health;

        animator = gameObject.GetComponent<Animator>();

        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = EnemyOBJ.MovementSpeed;

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
        Agent.isStopped = false;
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

        Vector3 LookPos = Player.position - transform.position;

        Quaternion LookRotation = Quaternion.LookRotation(LookPos);

        if (CurrentHealth <= 0)
        {
            ActiveState = State.Dead;
        }

        else if (PlayerDistance <= ChasePlayerRange && PlayerDistance > AttackRange)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, 5 * Time.deltaTime);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.ChaseRange);
    }
}
