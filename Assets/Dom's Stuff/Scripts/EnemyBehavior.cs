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
    [SerializeField] private GameObject Weapon = null;

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private float PlayerDistance = 0;
    private Vector3 SpawnLocation;

    private bool Alive = true;

    private enum State {Initial, Idle, Patrol, Attack, ChasePlayer, Dead };
    private State ActiveState = State.Initial; 

    private IEnumerator Start()
    {
        while (Alive == true)
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
                case State.Dead:
                    DoDeath();
                    break;
            }
            yield return null;
        }
    }

    private void Initial()
    {
        gameObject.tag = "Enemy";

        // Find the Player and enemy's fire point
        Player = PlayerRefs.instance.Player.gameObject;
        FirePoint = gameObject.transform.Find("Fire Point");

        // set health and current health
        Health = EnemyOBj.Health;
        CurrentHealth = Health;

        // Get Animator
        Animator = gameObject.GetComponentInChildren<Animator>();

        // Movement and Patrol Initialization
        Agent = GetComponent<NavMeshAgent>();
        Agent.autoBraking = true; // Enemy does not slow down when reaching point
        Agent.speed = EnemyOBj.MovementSpeed;

        SpawnLocation = transform.position;

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
        if (points.Length == 0)
            return;
        Agent.destination = points[DestinationPoint].position;
        DestinationPoint = (DestinationPoint + 1) % points.Length;
    }

    private void ChasePlayer()
    {
        Agent.destination = Player.transform.position;
    }

    private void StartAttack()
    {
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
                if(AttackCooldown <= 0)
                {
                    MeleeAttack();
                    AttackCooldown = 1 / EnemyOBj.AttackRate;
                }
                AttackCooldown -= Time.deltaTime;
                    break;
        }
    }

    private void RangedAttack()
    {
        transform.LookAt(Player.transform);
        Instantiate(EnemyOBj.ProjectileOBJ.Projectile, FirePoint.position, FirePoint.transform.rotation);
    }

    private void MeleeAttack()
    {
        transform.LookAt(Player.transform);
        Animator.SetTrigger("Swing Club");
    }

    private void DoDeath()
    {
        int RandomNumber = Random.Range(0, 100);
        if(RandomNumber <= EnemyOBj.PickupOBJ.DropChance)
        {
            Instantiate(EnemyOBj.PickupOBJ.PickupGameObject, transform.position + new Vector3 (0f, 0.5f, 0f), transform.rotation);
        }
        Agent.isStopped = true;
        Destroy(gameObject);
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

        float blend = Agent.velocity.magnitude / Agent.speed;
        Animator.SetFloat("Blend", blend , .1f, Time.deltaTime);

        if(CurrentHealth <= 0)
        {
            ActiveState = State.Dead;
        }

        // If the player is in range of chase distance AND is not dead, Chase Player
        if (PlayerDistance <= ChasePlayerRange && ActiveState != State.Dead)
        {
            ActiveState = State.ChasePlayer;
        }
        else
        {
            Agent.destination = SpawnLocation;
        }

        // If player is in attack range AND not dead, attack player
        if (PlayerDistance <= EnemyOBj.AttackRange && ActiveState != State.Dead)
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
