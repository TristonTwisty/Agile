using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class NewRingToss : MonoBehaviour
{
    [Header("View")]
    [SerializeField] [Tooltip("The object the ring follows")] private Transform RingHolster = null;
    [SerializeField] [Tooltip("The trail renderer of the disc")] private TrailRenderer TR = null;

    [Header("Mechanics")]
    [SerializeField] private float Damage;
    [SerializeField] [Tooltip("How fast the ring travels after being thrown")] private float ThrowSpeed;
    [SerializeField] [Tooltip("How how seconds it takes for ring to return to player")] private float ReturnSpeed = 0f;
    [SerializeField] [Tooltip("How far the disc can travel before returning to player")] private float MaxDistance = 0f;

    [Header("Lock On")]
    [SerializeField] private float LockOnDistance = 15;
    [SerializeField] private int MaxLockOn;
    private int CurrentLockOn;
    private List<Transform> LockOnTargets = new List<Transform>();

    private Rigidbody RB;
    private BoxCollider BC;
    private Vector3 LastVelocity;
    private bool Thrown = false; // Has the ring been thrown?
    private bool DoReturn = false;
    private Transform PlayerCamera;
    [HideInInspector] public bool ThirdPerson;

    private void Start()
    {
        // Get ring's collider and rigidbody
        // Set ring's collider to trigger
        RB = GetComponent<Rigidbody>();
        BC = GetComponent<BoxCollider>();
        TR.enabled = false;
        BC.isTrigger = true;
        //PlayerCamera = PlayerRefs.instance.PlayerCamera.gameObject;
        PlayerCamera = Camera.main.transform;

        // Move ring to player
        transform.position = RingHolster.position;

        ThirdPerson = false;
    }

    private void Update()
    {
        if (!Thrown && Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // If the ring was NOT thrown and player hits left mouse, throw ring
                if (!ThirdPerson)
                {
                    ThrowDisc();
                }
            }

            // If the ring has not been thrown and the player hits right mouse, begin locking onto targets
            if (Input.GetMouseButtonDown(1))
            {
                // If the player points at an enemy or a targetable object, add them to lock-on target list
                if(Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out RaycastHit hit, LockOnDistance))
                {
                    if(CurrentLockOn >= MaxLockOn)
                    {
                        if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Target"))
                        {
                            CurrentLockOn += 1;
                            LockOnTargets.Add(hit.transform);
                        }
                    }
                }
                if (Input.GetMouseButtonUp(1))
                {
                    // If the player did not lock onto any object just throw the disc normally
                    if(LockOnTargets == null)
                    {
                        ThrowDisc();
                    }
                    // If targets have been assigned begin lock-on attack
                    else if(LockOnTargets != null)
                    {
                        LockOnAttack();
                    }
                }
            }
        }
        if (!Thrown)
        {
            // If ring was not thrown, make the ring follow the ring holster
            transform.position = RingHolster.position;
        }
        if (Thrown)
        {
            // Check whether the ring was thrown
            if (Input.GetMouseButtonDown(1) || Vector3.Distance(transform.position, RingHolster.position) > MaxDistance)
            {
                // If the ring is too far from the player, return the ring to the player
                // If the player hits the right mouse, return the ring to the player
                DoReturn = true;
            }
        }
        if (DoReturn)
        {
            ReturnDisc();
        }

        LastVelocity = RB.velocity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (Thrown && other.gameObject.CompareTag("Player"))
        {
            // Change the ring's collider to collision when it leaves the player's collider
            BC.isTrigger = false;

            //Turn on trail renderer once the ring leaves the player
            TR.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Thrown && other.gameObject.CompareTag("Player"))
        {
            // Trail renderer turns off once it hits the player
            TR.enabled = false;

            Thrown = false;
            DoReturn = false;

            // Stop all physics movement of the ring
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;

            // Change ring's rotation to match holster
            transform.rotation = Quaternion.Euler(90, 0, 180);

            transform.position = Vector3.MoveTowards(transform.position, RingHolster.position, 1);
        }

        if(Thrown && other.gameObject.CompareTag("Enemy"))
        {
            // If the ring hits an enemy while returning to the player, deal damage
            other.gameObject.GetComponent<EnemyBehavior>().TakeDamage(Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float Speed = LastVelocity.magnitude;
        Vector3 Direction = Vector3.Reflect(LastVelocity.normalized, collision.contacts[0].normal);

        RB.velocity = Direction * Mathf.Max(Speed, 0);

        // If the ring hits the player, return ring to player
        if (collision.gameObject.CompareTag("Player"))
        {
            DoReturn = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If an enemy is hit, deal damage
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(Damage);
        }
    }

    private void ThrowDisc()
    {
        Thrown = true;
        RB.AddForce(PlayerCamera.forward * ThrowSpeed, ForceMode.Force);
    }

    private void LockOnAttack()
    {
        Thrown = true;
        while(LockOnTargets != null)
        {
            Vector3 Direction = LockOnTargets[0].position - transform.position;
            Direction.Normalize();
        }
    }

    private void ReturnDisc()
    {
        // Set ring back to trigger so it doesn't knock over player
        BC.isTrigger = true;
        Vector3 Destination = RingHolster.position - transform.position;
        RB.velocity = Destination * ReturnSpeed;
    }
}
