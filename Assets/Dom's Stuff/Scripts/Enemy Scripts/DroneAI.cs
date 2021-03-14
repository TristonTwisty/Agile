using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class DroneAI : MonoBehaviour
{
    [Header("Player info")]
    [SerializeField] private Transform Player;

    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    private float ChasePlayerRange;
    private float AttackRange;
    private float Health = 0;

    [Header("Components")]
    private EnemyBehavior EB;
    private EnemyScripableObject EnemyOBJ;
    private Rigidbody RB;

    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;
    private Quaternion LookRotation;

    [Header("Movement")]
    [SerializeField] [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] private Transform[] points;
    private int DestinationPoint = 0;
    private Vector3 SpawnLocation;
    private float MovementSpeed = 0;

    [Header("Shooter")]
    [Tooltip("Where the projectiles come from")] public Transform FirePoint = null;
    private float AttackCooldown = 0;
    private float BulletsPerShot;
    private float Bullets = 0;

    [Header("Drone")]
    [Tooltip("How close to the ground can this enemy get before it's pushed back up?")] [SerializeReference] float HoverHeight = 1;
    [SerializeField] float HoverForce;
    private bool IsStopped = false;

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

        gameObject.tag = "Enemy";

        EB = GetComponent<EnemyBehavior>();
        EnemyOBJ = EB.EnemyOBJ;

        Health = EB.CurrentHealth;

        animator = gameObject.GetComponent<Animator>();

        SpawnLocation = transform.position;

        ChasePlayerRange = EnemyOBJ.ChaseRange;
        AttackRange = EnemyOBJ.AttackRange;

        MovementSpeed = EnemyOBJ.MovementSpeed;

        BulletsPerShot = EnemyOBJ.BulletsPerShot;

        RB = GetComponent<Rigidbody>();

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

        Vector3 Directtion = points[DestinationPoint].position - transform.position;
        Directtion.Normalize();
        Vector3 Velocity = Directtion * MovementSpeed;
        RB.velocity = Velocity;

        DestinationPoint = (DestinationPoint + 1) % points.Length;
    }

    private void Chase()
    {
        Vector3 ChaseDirection;
        Vector3 Velocity;

        if (!IsStopped)
        {
            ChaseDirection = Player.position - transform.position;
            ChaseDirection.Normalize();
            Velocity = ChaseDirection * MovementSpeed;
            RB.velocity = Velocity;
        }
        else
        {
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
        }
    }

    private void StartAttack()
    {
        FirePoint.LookAt(Player);

        if (AttackCooldown <= 0)
        {
            Shoot();
            AttackCooldown = 1 / EnemyOBJ.AttackRate;
        }
        AttackCooldown -= Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        while (Bullets < BulletsPerShot)
        {
            ObjectPooling.Spawn(EnemyOBJ.ProjectileOBJ.Projectile, FirePoint.position, FirePoint.rotation);
            Bullets += 1;
        }
        if (Bullets >= BulletsPerShot)
        {
            Bullets = 0;
        }
    }

    private void DoDeath()
    {
        RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;

        int RandomNumber = Random.Range(0, 100);
        if (RandomNumber <= EnemyOBJ.PickupOBJ.DropChance)
        {
            Instantiate(EnemyOBJ.PickupOBJ.PickupGameObject, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        // Distance betyween AI and player
        PlayerDistance = Vector3.Distance(transform.position, Player.position);


        Health = EB.CurrentHealth;

        if (Health <= 0)
        {
            ActiveState = State.Dead;
            IsStopped = true;
        }

        // If the player is within the chase range but not the attack range, chase
        else if (PlayerDistance <= ChasePlayerRange && PlayerDistance > AttackRange)
        {
            ActiveState = State.Chase;
            IsStopped = false;
        }
        // If the player is in attack range, attack
        else if (PlayerDistance <= AttackRange)
        {
            ActiveState = State.Attack;
            IsStopped = true;
        }

        // If the player leaves the chase range and the AI was idling, return to spawn point
        else if (PlayerDistance > ChasePlayerRange && Idling)
        {
            if (transform.position != SpawnLocation)
            {
                Vector3 Direction = SpawnLocation - transform.position;
                Direction.Normalize();
                Vector3 Velocity = Direction * MovementSpeed;
                RB.velocity = Velocity;
            }
        }

        // If the drone is too close to the ground, push it upwards
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit Ground, HoverHeight))
        {
            float GroundDistance = Vector3.Distance(transform.position, Ground.transform.position);

            if(GroundDistance < HoverHeight)
            {
                RB.AddForce(transform.up * HoverForce, ForceMode.Impulse);
            }
            if(GroundDistance >= HoverHeight)
            {
                RB.velocity = Vector3.zero;
                RB.angularVelocity = Vector3.zero;
            }
        }

        // If the drone is too close to the ceiling, push it downwards
        if (Physics.Raycast(transform.position, transform.up, out RaycastHit Ceiling, HoverHeight))
        {
            float GroundDistance = Vector3.Distance(transform.position, Ground.transform.position);

            if (GroundDistance < HoverHeight)
            {
                RB.AddForce(-transform.up * HoverForce, ForceMode.Impulse);
            }
            if (GroundDistance >= HoverHeight)
            {
                RB.velocity = Vector3.zero;
                RB.angularVelocity = Vector3.zero;
            }
        }
    }
}
