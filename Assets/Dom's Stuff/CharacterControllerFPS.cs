using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerFPS : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MovementSpeed = 10;
    [SerializeField] private float JumpHeight = 5;

    [Header("Components")]
    private CharacterController CharacterController;
    public Animator Animator;

    [Header("Gravity")]
    [SerializeField] private float Gravity = 10;
    private bool Grounded;
    private Vector3 PlayerVelocity;

    [Header("Wall Walkling")]
    public bool CanWallWalk;
    [Tooltip("How close the player's feet have to be to the surface to lock onto it")] [SerializeField] private float GravityLock = 2;
    [Tooltip("How smooth the player rotates when latching to a surface")] [SerializeField] private float LerpSpeed = 5;
    private Vector3 SurfaceNormal;
    private Vector3 MyNormal;
    private float GroundDistance;
    private float DeltaGround = .2f;
    private bool IsGrounded;

    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Movement
        Grounded = CharacterController.isGrounded;
        if (Grounded && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;
        Vector3 Move = transform.right * x + transform.forward * z;

        CharacterController.Move(Move);

        // Jump
        if (Input.GetKey(KeyCode.Space) && Grounded)
        {
            PlayerVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * -Gravity);
        }

        PlayerVelocity.y += -Gravity * Time.deltaTime;
        CharacterController.Move(PlayerVelocity * Time.deltaTime);

        //Wall Walking
        Ray ray;
        RaycastHit Hit;

        ray = new Ray(transform.position, -MyNormal);
        if (Physics.Raycast(ray, out Hit, GravityLock))
        {
            if (CanWallWalk)
            {
                if (Hit.transform.gameObject.layer == 18)
                {
                    IsGrounded = Hit.distance <= GroundDistance + DeltaGround;
                    SurfaceNormal = Hit.normal;
                }
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
}
