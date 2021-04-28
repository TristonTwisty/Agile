using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ParticalDisk : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool Testing = false;

    [Header("Holster")]
    [SerializeField] private Transform RingHolster;
    private Transform Cam;

    [Header("Components")]
    private TrailRenderer trailRenderer;
    private Rigidbody rigidbody;
    private Collider collider;

    [Header("Ring Behavior")]
    [SerializeField] private float ThrowSpeed = 5;
    [SerializeField] private float ReturnSpeed = 2f;
    [SerializeField] private float Damage = 5;
    [SerializeField] private float MaxDistance = 20;
    [SerializeField] private bool Thrown = false;
    [SerializeField] private bool ToBase = true;
    private Vector3 LastVelocity;

    [Header("Lock On")]
    [SerializeField] private float LockOnDistance = 15;
    public int MaxLockOn = 0;
    [SerializeField] private LayerMask IgnoreMask;
    private List<Transform> LockOnTargets = new List<Transform>();
    private List<RingTarget> _ringTarget = new List<RingTarget>();

    private void Start()
    {
        Cam = Camera.main.transform;

        rigidbody = GetComponent<Rigidbody>();

        collider = GetComponent<Collider>();
        collider.isTrigger = true;

        trailRenderer = GetComponent<TrailRenderer>();

        if (!Testing)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        transform.parent = null;
        transform.position = RingHolster.position;
    }

    void Update()
    {
        if (!Thrown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ThrowDisk();
            }
            if (Input.GetMouseButton(1))
            {
                if(MaxLockOn == 0)
                {
                    ThrowDisk();
                }

                // If the player points at an enemy or a targetable object, add them to lock-on target list
                if (Physics.Raycast(Cam.position, Cam.forward, out RaycastHit hit, LockOnDistance, ~IgnoreMask))
                {
                    Debug.Log(hit.transform.name);

                    if (LockOnTargets.Count < MaxLockOn)
                    {
                        if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Target"))
                        {
                            if (!LockOnTargets.Contains(hit.transform))
                            {
                                LockOnTargets.Add(hit.transform);
                                _ringTarget.Add(new RingTarget(hit.transform.GetComponentInChildren<TargetingMarker>()));
                                hit.transform.GetComponentInChildren<TargetingMarker>().ShowMarkers = true;
                            }
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                // If the player did not lock onto any object just throw the disc normally
                if (LockOnTargets == null)
                {
                    ThrowDisk();
                }
                // If targets have been assigned begin lock-on attack
                else if (LockOnTargets != null)
                {
                    StartCoroutine(LockOnAttack());
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && Thrown)
        {
            ToBase = true;
        }
        if (ToBase)
        {
            ReturnDisk();
        }
        if(ToBase && !Thrown)
        {
            trailRenderer.enabled = false;
        }
        if (Thrown)
        {
            trailRenderer.enabled = true;
        }
    }

    private void ThrowDisk()
    {
        collider.isTrigger = false;
        Thrown = true;
        ToBase = false;
        rigidbody.AddForce(RingHolster.forward * ThrowSpeed, ForceMode.VelocityChange);
    }

    private IEnumerator LockOnAttack()
    {
        Thrown = true;
        float t = 0;
        int curIndex = 0;

        //dumby value
        float speed = 25;

        Vector3 startPosition;
        float currentDistance;
        float timeToReachDistance;

        while (curIndex < LockOnTargets.Count)
        {
            t = 0;
            startPosition = transform.position;
            currentDistance = Vector3.Distance(startPosition, LockOnTargets[curIndex].position);
            timeToReachDistance = currentDistance / speed;

            while (t < timeToReachDistance)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, LockOnTargets[curIndex].position, t / timeToReachDistance);
                yield return null;
            }

            curIndex++;
            yield return null;
        }

        LockOnTargets.Clear();

        for (int i = 0; i < _ringTarget.Count; i++)
        {
            if (_ringTarget[i] != null && _ringTarget[i].targetingMarker != null)
            {
                _ringTarget[i].targetingMarker.ShowMarkers = false;
            }
        }
        _ringTarget.Clear();

        ToBase = true;
    }

    private void ReturnDisk()
    {
        collider.isTrigger = true;
        Vector3 Destination = RingHolster.position - transform.position;
        rigidbody.velocity = Destination * ReturnSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RingHolster"))
        {
            if (Thrown && ToBase)
            {
                Thrown = false;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }

        if (!ToBase)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyBehavior>().TakeDamage(Damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float Speed = LastVelocity.magnitude;
        Vector3 Direction = Vector3.Reflect(transform.position.normalized, collision.contacts[0].normal);

        rigidbody.AddForce(Direction * ThrowSpeed, ForceMode.VelocityChange);

        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(Damage);
        }
    }

    public class RingTarget
    {
        public TargetingMarker targetingMarker;

        public RingTarget(TargetingMarker _targetingMarker)
        {
            targetingMarker = _targetingMarker;
        }
    }
}
