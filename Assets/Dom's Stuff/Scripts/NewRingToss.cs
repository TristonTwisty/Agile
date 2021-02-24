using UnityEngine;
using System.Collections;
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
    private List<Transform> LockOnTargets = new List<Transform>();
    private List<RingTarget> _ringTarget = new List<RingTarget>();

    private Rigidbody RB;
    private BoxCollider BC;
    private Vector3 LastVelocity;
    private bool Thrown = false; // Has the ring been thrown?
    private bool DoReturn = false;
    private Transform PlayerCamera;

    [SerializeField]private Color _lockOnColour;

    [HideInInspector] public bool ThirdPerson;


    //Added For GameSounds
    private GameSounds gameSounds;

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


        //Added For GameSounds
        gameSounds = GameSounds.FindObjectOfType<GameSounds>();
    }

    private void Update()
    {
        if (Thrown == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
//<<<<<<< HEAD
                ThrowDisc();

                //Added For GameSounds
                gameSounds.audioSource.PlayOneShot(gameSounds.discTossed);
//=======
                // If the ring was NOT thrown and player hits left mouse, throw ring
                if (!ThirdPerson)
                {
                    ThrowDisc();
                }
//>>>>>>> fd995de88b3b738846d8733ee7054a74d5d20829
            }

            // If the ring has not been thrown and the player hits right mouse, begin locking onto targets
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse Down");

                // If the player points at an enemy or a targetable object, add them to lock-on target list
                if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out RaycastHit hit, LockOnDistance))
                {
                    Debug.Log(hit.transform.name);

                    if (LockOnTargets.Count < MaxLockOn)
                    {
                        if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Target"))
                        {
                            if (!LockOnTargets.Contains(hit.transform))
                            {
                                LockOnTargets.Add(hit.transform);
                                _ringTarget.Add(new RingTarget(hit.transform.GetComponent<MeshRenderer>(), hit.transform.GetComponent<MeshRenderer>().material.color));
                                hit.transform.GetComponent<MeshRenderer>().material.color = _lockOnColour;
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
                    ThrowDisc();
                }
                // If targets have been assigned begin lock-on attack
                else if (LockOnTargets != null)
                {
                    StartCoroutine(LockOnAttack());
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

        if(Thrown && other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyBehavior>() != null)
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

        if (collision.gameObject.CompareTag("Enemy") && GetComponent<EnemyBehavior>() != null)
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

    private IEnumerator LockOnAttack()
    {
        Debug.Log("Count: " + LockOnTargets.Count);

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
            if(_ringTarget[i] != null && _ringTarget[i].meshRenderer != null)
            {
                _ringTarget[i].meshRenderer.material.color = _ringTarget[i].colour;
            }
        }
        _ringTarget.Clear();

        ReturnDisc();
    }

    private void ReturnDisc()
    {
        // Set ring back to trigger so it doesn't knock over player
        BC.isTrigger = true;
        Vector3 Destination = RingHolster.position - transform.position;
        RB.velocity = Destination * ReturnSpeed;
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
