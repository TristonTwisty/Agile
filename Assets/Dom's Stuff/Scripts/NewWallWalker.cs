using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class NewWallWalker : MonoBehaviour
{
    private Transform Cam;

    [Header("Movement")]
    [Tooltip("How fast can the player move forward and backward")] [SerializeField] private float MovementSpeed = 10;
    [Tooltip("How fast can the player move side to side")] [SerializeField] private float StrafeSpeed = 8;
    [Tooltip("How smooth the player rotates when latching to a surface")] [SerializeField] private float LerpSpeed = 5;
    [SerializeField] private float JumpHeight = 350;
    public bool CanWallWalk = false;
    public bool IsJumping = false;

    [SerializeField] private float TurnSpeed = 100;

    [Header("Gravity")]
    [SerializeField] private float Gravity = 10;
    private bool IsGrounded;
    private float DeltaGround = .2f;

    [Header("Ground")]
    [Tooltip("What can the player latch to?")] [SerializeField] private LayerMask Walkable;
    [Tooltip("How close the player's feet have to be to the surface to lock onto it")] [SerializeField] private float GravityLock = 2;
    private Vector3 SurfaceNormal;
    private Vector3 MyNormal;
    private float GroundDistance;

    [Header("Components")]
    private Rigidbody RB;
    private CapsuleCollider CC;

    private void Start()
    {
        gameObject.tag = "Player";

        if(gameObject.tag != "Player")
        {
            Debug.Log("Player is not tagged as playe!");
        }

        // Get main camera
        Cam = Camera.main.transform;

        // Get rigibody and collider
        RB = GetComponent<Rigidbody>();
        CC = GetComponent<CapsuleCollider>();


        MyNormal = transform.up;
        RB.freezeRotation = true;
        RB.useGravity = false;
        GroundDistance = CC.bounds.extents.y - CC.center.y;
    }

    private void FixedUpdate()
    {
        RB.AddForce(-Gravity * RB.mass * MyNormal);
    }

    private void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && IsJumping == false)
        {
            IsJumping = true;
            RB.AddForce(transform.up * JumpHeight, ForceMode.Force);
        }

        float VelocityZ = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        float VelocityX = Input.GetAxis("Horizontal") * StrafeSpeed * Time.deltaTime;
        Vector3 MovementDir = new Vector3(VelocityX, 0, VelocityZ);

        // Movement
        transform.Translate(MovementDir);

        // Gravity
        Ray ray;
        RaycastHit Hit;

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
        if (collision.gameObject.layer == 10)
        {
            IsJumping = false;
        }
        else
        {
            Debug.Log("Is the ground layer set to to right int?");
        }
    }
}
