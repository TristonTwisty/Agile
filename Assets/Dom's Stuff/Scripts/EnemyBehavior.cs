using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject Player = null;

    [Header("Enemy Statistics")]
    public EnemyScripableObject EnemyOBj;
    public enum AttackType {MeleeHumanoid, RangeHumanoid,};
    [Tooltip("Will this character shoot or melee?")] public AttackType EnemyType;
    [Tooltip("How close does the player need to get to cause this enemy to chase them? Should always be higher than the attack range")] public float ChasePlayerRange;
    private Transform FirePoint = null; // The barrel of the gun. Comes with the Ranged Enemy Prefab
    private float AttackCooldown = 0;
    private float Health = 0;
    private float CurrentHealth = 0;
    private Animator Animator;

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private float PlayerDistance = 0;

    private bool Alive = true;

    private enum State {Initial, Idle, Patrol, Attack, ChasePlayer };
    private State ActiveState = State.Initial;

    private IEnumerator Start()
    {
        while(Alive == true)
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
                case State.Attack:
                    StartAttack();
                    break;
                case State.ChasePlayer:
                    ChasePlayer();
                    break;
            }
            yield return null;
        }
    }

    private void Initial()
    {
        Debug.Log("In Initial");

        // Find the Player and enemy's fire point
        Player = GameObject.Find("Player Target");
        FirePoint = gameObject.transform.Find("Fire Point");

        // Get Animator
        Animator = GetComponent<Animator>();

        // set health and current health
        Health = EnemyOBj.Health;
        CurrentHealth = Health;

        // Movement and Patrol Initialization
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = false; // Enemy does not slow down when reaching point
        Agent.speed = EnemyOBj.MovementSpeed;

        // If no patrol points were set the enemy stands still
        if (points == null)
        {
            ActiveState = State.Idle;
        }
        else
        {
            ActiveState = State.Patrol;
        }
    }

    private void Idle()
    {
        Debug.Log("In Idle");
    }

    private void Patrol()
    {
        Debug.Log("In Patrol");
        if (points.Length == 0)
            return;
        Agent.destination = points[DestinationPoint].position;
        DestinationPoint = (DestinationPoint + 1) % points.Length;
    }

    private void ChasePlayer()
    {
        Debug.Log("In ChasePlayer");
        Agent.destination = Player.transform.position;
    }

    private void StartAttack()
    {
        Debug.Log("In StartAttack");

        switch (EnemyType)
        {
            case AttackType.RangeHumanoid:
                if (AttackCooldown <= 0)
                {
                    RangedAttack();
                    AttackCooldown = 1 / EnemyOBj.AttackRate;
                }
                AttackCooldown -= Time.deltaTime;
                break;

            case AttackType.MeleeHumanoid:
                MeleeAttack();
                    break;
        }
    }

    private void RangedAttack()
    {
        Debug.Log("Shoot");
        transform.LookAt(Player.transform);
        Instantiate(EnemyOBj.ProjectileOBJ.Projectile, FirePoint.position, FirePoint.transform.rotation);
    }

    private void MeleeAttack()
    {
        Animator.Play("Swing Club");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBj.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChasePlayerRange);
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.transform.position);

        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (PlayerDistance <= ChasePlayerRange)
        {
            ActiveState = State.ChasePlayer;
        }
        else if (points == null)
        {
            ActiveState = State.Idle;
        }
        else
        {
            ActiveState = State.Patrol;
        }

        if (PlayerDistance <= EnemyOBj.AttackRange)
        {
            ActiveState = State.Attack;
            Agent.isStopped = true;
        }
        else
        {
            Agent.isStopped = false;
            if(ActiveState == State.Attack)
            {
                ActiveState = State.ChasePlayer;
            }
        }
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }
}
