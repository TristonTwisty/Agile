using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    [Header("Player info")]
    [SerializeField] private Transform Player;
    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    private float ChasePlayerRange;
    private float AttackRange;
    private float Health = 0;
    private float CurrentHealth = 0;
    private LayerMask MyLayer;

    [Header("Components")]
    public EnemyScripableObject EnemyOBJ;
    [Tooltip("The enemy's face, where they look")] [SerializeField] private Transform Face = null;
    private Rigidbody RB;

    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private Vector3 SpawnLocation;
    private float MovementSpeed = 0;
    [Tooltip("How close to the ground can this enemy get before it's pushed back up?")] [SerializeReference] float HoverHeight = 1;

    [Header("Shooter")]
    [Tooltip("Where the projectiles come from")] public Transform FirePoint = null;
    private float AttackCooldown = 0;
    private float BulletsPerShot;
    private float Bullets = 0;

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
        //Player = PlayerRefs.instance.Player;

        gameObject.tag = "Shooter Enemy";

        Health = EnemyOBJ.Health;
        CurrentHealth = Health;

        animator = gameObject.GetComponent<Animator>();

        SpawnLocation = transform.position;

        ChasePlayerRange = EnemyOBJ.ChaseRange;
        AttackRange = EnemyOBJ.AttackRange;

        MovementSpeed = EnemyOBJ.MovementSpeed;

        BulletsPerShot = EnemyOBJ.BulletsPerShot;

        MyLayer = gameObject.layer;

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
        transform.position = Vector3.MoveTowards(transform.position, points[DestinationPoint].position, MovementSpeed * Time.deltaTime);
        DestinationPoint = (DestinationPoint + 1) % points.Length;
    }

    private void Chase()
    {
        transform.LookAt(Player.position);
        transform.position = Vector3.MoveTowards(transform.position, Player.position, MovementSpeed * Time.deltaTime);
    }

    private void StartAttack()
    {
        transform.LookAt(Player.position);
        if (AttackCooldown <= 0)
        {
            Shoot();
            AttackCooldown = 1 / EnemyOBJ.AttackRate;
        }
        AttackCooldown -= Time.deltaTime;
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
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -transform.up * HoverHeight);
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.position);

        if (CurrentHealth <= 0)
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
                transform.position = Vector3.MoveTowards(transform.position, SpawnLocation, MovementSpeed * Time.deltaTime);
            }
        }

        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit Ground, Mathf.Infinity))
        {
            float GroundDistance = Vector3.Distance(transform.position, Ground.transform.position);

            if(GroundDistance < HoverHeight)
            {
                RB.AddForce(transform.up * 50, ForceMode.Acceleration);
            }
        }
    }
}
