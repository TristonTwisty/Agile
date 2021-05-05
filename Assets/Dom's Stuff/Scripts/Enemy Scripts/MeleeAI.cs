using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyBehavior))]
[RequireComponent(typeof(Rigidbody))]
public class MeleeAI : MonoBehaviour
{
    [Header("Testing")]
    [Tooltip("Toggle when wanted to test AI without loading whole game")] public bool AiTest = false;

    [Header("Player info")]
    [SerializeField] private Transform Player;
    private float PlayerDistance = 0;

    [Header("Enemy Statistics")]
    private EnemyBehavior EB;
    private EnemyScripableObject EnemyOBJ;
    private float ChasePlayerRange;
    private float AttackRange;
    [Tooltip("The enemy's face, where they look")] public Transform Face = null;
    private float Health;

    [Header("Behavior")]
    private Animator animator;
    private bool Idling = false;
    private bool CanAttack = true;

    [Header("Components")]
    private Collider MainCollider;
    private Rigidbody MainRigibody;

    [Header("Movement")]
    [Tooltip("If you want the enemy to patrol place the transforms here. Leave empty to have enemy idle")] public Transform[] points;
    private int DestinationPoint = 0;
    private NavMeshAgent Agent;
    private Vector3 SpawnLocation;

    [Header("Melee")]
    [SerializeField] private GameObject Weapon = null;

    [Header("Ragdoll")]
    [SerializeField] private List<Rigidbody> RagdollBodies = new List<Rigidbody>();
    [SerializeField] private List<Collider> RagdollColliders = new List<Collider>();

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
        if (!AiTest)
        {
            Player = PlayerRefs.instance.Player;
        }

        EB = GetComponent<EnemyBehavior>();
        EnemyOBJ = EB.EnemyOBJ;

        Health = EB.CurrentHealth;

        gameObject.tag = "Enemy";

        animator = gameObject.GetComponent<Animator>();

        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = EnemyOBJ.MovementSpeed;

        SpawnLocation = transform.position;

        ChasePlayerRange = EnemyOBJ.ChaseRange;
        AttackRange = EnemyOBJ.AttackRange;

        MainRigibody = GetComponent<Rigidbody>();
        MainCollider = GetComponent<Collider>();

        RagdollBodies = GetComponentsInChildren<Rigidbody>().ToList();
        RagdollBodies.Remove(MainRigibody);

        RagdollColliders = GetComponentsInChildren<Collider>().ToList();
        RagdollColliders.Remove(MainCollider);

        ToggleRagdoll(false);

        if (ChasePlayerRange <= AttackRange)
        {
            ChasePlayerRange = 1 + AttackRange;
        }

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

        Vector3 LookPos = Player.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(LookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, 5 * Time.deltaTime);

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

        Vector3 LookPos = Player.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(LookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, 5 * Time.deltaTime);
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
        IsAlive = false;
        Agent.isStopped = true;
        int RandomNumber = Random.Range(0, 100);
        if (RandomNumber <= EnemyOBJ.PickupOBJ.DropChance)
        {
            ObjectPooling.Spawn(EnemyOBJ.PickupOBJ.PickupGameObject, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
        ToggleRagdoll(true);
        foreach (Rigidbody Bodies in RagdollBodies)
        {
            Bodies.AddExplosionForce(107, Player.position, 6, 1, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, Player.position);

        Health = EB.CurrentHealth;

        Vector3 LookPos = Player.position - transform.position;

        Quaternion LookRotation = Quaternion.LookRotation(LookPos);

        if (Health <= 0)
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

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach (Rigidbody RB in RagdollBodies)
        {
            RB.isKinematic = !state;
            RB.useGravity = state;

            MainRigibody.isKinematic = state;
            MainRigibody.useGravity = !state;
        }

        foreach (Collider collider in RagdollColliders)
        {
            collider.enabled = state;
            MainCollider.enabled = !state;
        }
    }
}
