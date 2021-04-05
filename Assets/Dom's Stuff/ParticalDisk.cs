using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ParticalDisk : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform RingHolster;
    private Transform Player;

    [Header("Components")]
    private TrailRenderer trailRenderer;
    private Rigidbody rigidbody;
    private Collider collider;

    [Header("Statistics")]
    [SerializeField] private float ThrowSpeed = 5;
    [SerializeField] private float ReturnSpeed = 5;
    [SerializeField] private float Damage = 5;
    [SerializeField] private float MaxDistance = 20;
    private Vector3 LastVelocity;

    [Header("Lock On")]
    [SerializeField] private float LockOnDistance = 15;
    [SerializeField] private int MaxLockOn = 3;
    private List<Transform> LockOnTargets = new List<Transform>();
    private List<RingTarget> _ringTarget = new List<RingTarget>();

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        trailRenderer = GetComponent<TrailRenderer>();

        //gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = RingHolster.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowDisk();
        }
    }

    private void ThrowDisk()
    {
        rigidbody.AddForce(RingHolster.forward * ThrowSpeed, ForceMode.VelocityChange);
    }

    public class RingTarget
    {
        public MeshRenderer meshRenderer;
        public Color colour;

        public RingTarget(MeshRenderer _meshRenderer, Color _colour)
        {
            meshRenderer = _meshRenderer;
            colour = _colour;
        }
    }
}
