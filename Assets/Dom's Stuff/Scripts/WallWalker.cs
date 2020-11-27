using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallWalker : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MovementSpeed = 6;
    [SerializeField] private float StrafeSpeed = 3;
    [SerializeField] private float TurnSpeed = 90;
    [SerializeField] private float LerpSpeed = 10;
    [SerializeField] private float JumpSpeed = 10;
    [SerializeField] private float JumpRange = 10;
    private float VerticleSpeed = 0;
    private bool IsJumping = false;

    [Header("Gravity")]
    [SerializeField] private float Gravity = 10;
    private bool IsGrounded;
    [SerializeField] private float DeltaGround = .2f;

    [Header("Ground")]
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
        if (IsJumping)
            return;

        Ray ray;
        RaycastHit Hit;

        transform.Translate(Input.GetAxis("Horizontal") , 0 , 0 * StrafeSpeed * Time.deltaTime);

        ray = new Ray(transform.position, -MyNormal);
        if(Physics.Raycast(ray, out Hit))
        {
            IsGrounded = Hit.distance <= GroundDistance + DeltaGround;
            SurfaceNormal = Hit.normal;
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

        transform.Translate(0, 0, Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime);
    }
}
