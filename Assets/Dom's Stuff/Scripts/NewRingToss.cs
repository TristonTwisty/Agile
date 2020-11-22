using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class NewRingToss : MonoBehaviour
{
    [Header("View")]
    [SerializeField] [Tooltip("The object the ring follows")] private Transform RingHolster = null;
    [SerializeField] [Tooltip("The trail renderer of the disc")] private TrailRenderer TR = null;

    [Header("Mechanics")]
    [SerializeField] private ProjectileScriptableObjects ProjectileOBJ;
    [Tooltip("How fast the ring travels after being thrown")] private float ThrowSpeed;
    [SerializeField] [Tooltip("How how seconds it takes for ring to return to player")] private float ReturnSpeed = 0f;
    [SerializeField] [Tooltip("How far the disc can travel before returning to player")] private float MaxDistance = 0f;

    private Rigidbody RB;
    private BoxCollider BC;
    private Vector3 LastVelocity;
    private bool Thrown = false; // Has the ring been thrown?
    private bool DoReturn = false;
    private GameObject PlayerCamera;
    [HideInInspector] public bool ThirdPerson;

    private void Start()
    {
        // Get ring's collider and rigidbody
        // Set ring's collider to trigger
        RB = GetComponent<Rigidbody>();
        BC =GetComponent<BoxCollider>();
        TR.enabled = false;
        BC.isTrigger = true;
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        //Set ring throwspeed
        ThrowSpeed = ProjectileOBJ.ProjectileSpeed;

        // Move ring to player
        transform.position = RingHolster.position;

        ThirdPerson = false;
    }

    private void Update()
    {
        if (!Thrown && Input.GetMouseButtonDown(0))
        {
            // If the ring was NOT thrown and player hits left mouse, throw ring
            if (!ThirdPerson)
            {
                ThrowDisc();
            }
            else
            {
                StraightShot();
            }
        }
        if (!Thrown)
        {
            // If ring was not thrown, make the ring follow the ring holster
            transform.position = Vector3.MoveTowards(transform.position, RingHolster.position, 1);
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

        // Call the take damage method after touching an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }

    private void ThrowDisc()
    {
        Thrown = true;
        RB.AddForce(PlayerCamera.transform.forward * ThrowSpeed, ForceMode.Force);
    }

    private void StraightShot()
    {
        Thrown = true;
        RB.AddForce(transform.up * ThrowSpeed, ForceMode.Force);
    }

    private void ReturnDisc()
    {
        // Set ring back to trigger so it doesn't knock over player
        BC.isTrigger = true;
        Vector3 Destination = RingHolster.position - transform.position;
        RB.velocity = Destination * ReturnSpeed;
    }
}
