using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class NewRingToss : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float BounceAmount = 500;
    [SerializeField] private float ReturnSpeed;
    [SerializeField] [Tooltip("How far the disc can travel before returning to player")] private float MaxDistance;

    private Rigidbody RB;
    private BoxCollider BC;
    private Vector3 LastVelocity;
    private bool Thrown = false;
    private bool DoReturn = false;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        BC =GetComponent<BoxCollider>();
        BC.isTrigger = true;
    }

    private void Update()
    {
        if (!Thrown && Input.GetMouseButtonDown(0))
        {
            ThrowDisc();
        }
        if (Thrown)
        {
            if (Input.GetMouseButtonDown(1) || Vector3.Distance(transform.position, Player.position) > MaxDistance)
            {
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
        if (other.gameObject.CompareTag("Player"))
        {
            BC.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Thrown = false;
            DoReturn = false;
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, Player.position, 1);
            transform.parent = Player;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float Speed = LastVelocity.magnitude;
        Vector3 Direction = Vector3.Reflect(LastVelocity.normalized, collision.contacts[0].normal);

        RB.velocity = Direction * Mathf.Max(Speed, 0);
    }

    private void ThrowDisc()
    {
        Thrown = true;
        transform.parent = null;
        RB.AddForce(Camera.main.transform.forward * BounceAmount, ForceMode.Force);
    }

    private void ReturnDisc()
    {
        BC.isTrigger = true;
        Vector3 Destination = Player.position - transform.position;
        RB.velocity = Destination * ReturnSpeed;
    }
}
