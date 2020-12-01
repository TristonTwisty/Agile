using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallWalker : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("How fast can the player move forward and backward")] [SerializeField] private float MovementSpeed = 6;
    [Tooltip("How fast can the player move side to side")] [SerializeField] private float StrafeSpeed = 3;
    [Tooltip("How smooth the player rotates when latching to a surface")] [SerializeField] private float LerpSpeed = 5;
    [SerializeField] private float JumpHeight = 10;
    [HideInInspector] public bool CanWallWalk = false;
    private bool IsJumping = false;

    [Header("Gravity")]
    [SerializeField] private float Gravity = 10;
    private bool IsGrounded;
    private float DeltaGround = .2f;

    [Header("Ground")]
    [Tooltip("What can the player latch to?")] [SerializeField] private LayerMask Walkable;
    [Tooltip("How close the player's feet have to be to the surface to lock onto it")] [SerializeField] private float GravityLock = 1;
    private Vector3 SurfaceNormal;
    private Vector3 MyNormal;
    private float GroundDistance;

    [Header("Components")]
    private Rigidbody RB;
    private BoxCollider BC;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        BC = GetComponent<BoxCollider>();

        MyNormal = transform.up;
        RB.freezeRotation = true;
        GroundDistance = BC.bounds.extents.y - BC.center.y;
    }

    private void FixedUpdate()
    {
        RB.AddForce(-Gravity * RB.mass * MyNormal);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsJumping == false)
        {
            IsJumping = true;
            RB.AddForce(transform.up * JumpHeight, ForceMode.Force);
        }

        Ray ray;
        RaycastHit Hit;

        transform.Translate(Input.GetAxis("Horizontal") * StrafeSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime);

        ray = new Ray(transform.position, -MyNormal);
        if (Physics.Raycast(ray, out Hit, GravityLock, Walkable))
         {
            if (CanWallWalk)
            {
                IsGrounded = Hit.distance <= GroundDistance + DeltaGround;
                SurfaceNormal = Hit.normal;
            }
         }
            else
            {
                IsGrounded = false;
                SurfaceNormal = Vector3.up;
            }

        MyNormal = Vector3.Lerp(MyNormal, SurfaceNormal, LerpSpeed * Time.deltaTime);
        Vector3 MyForward = Vector3.Cross(transform.right, MyNormal);
        Quaternion TargetRotation = Quaternion.LookRotation(MyForward, MyNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, LerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == Walkable)
        {
            IsJumping = false;
        }
    }
}
