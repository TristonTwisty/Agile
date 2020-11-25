using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallWalker : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float MovementSpeed = 6;
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

        if (Input.GetButtonDown("Jump"))
        {
            ray = new Ray(transform.position, -MyNormal);

            if (Physics.Raycast(ray, out Hit, JumpRange))
            {
                JumpToWall(Hit.point, Hit.normal);
            }
            else if(IsGrounded)
            {
                RB.velocity += JumpSpeed * MyNormal;
            }
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * TurnSpeed * Time.deltaTime, 0);

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

    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        IsJumping = true;
        RB.isKinematic = true;
        Vector3 OriginalPos = transform.position;
        Quaternion OriginalRotation = transform.rotation;
        Vector3 DistantPos = point + normal * (GroundDistance + 0.5f);
        Vector3 MyForward = Vector3.Cross(transform.right, normal);
        Quaternion DistanceRot = Quaternion.LookRotation(MyForward, normal);

        StartCoroutine(JumpTime(OriginalPos, OriginalRotation, DistantPos, DistanceRot, normal));
    }

    private IEnumerator JumpTime(Vector3 OriginalPos, Quaternion OriginalRotation, Vector3 DistantPos, Quaternion DistanceRot, Vector3 normal)
    {
        for(float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(OriginalPos, DistantPos, t);
            transform.rotation = Quaternion.Slerp(OriginalRotation, DistanceRot, t);
            yield return null;
        }
        MyNormal = normal;
        RB.isKinematic = false;
        IsJumping = false;
    }
}
