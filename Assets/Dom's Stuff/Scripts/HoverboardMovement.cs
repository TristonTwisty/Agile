//All the physics behaviors for the board

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class HoverboardMovement : MonoBehaviour
{
    [Tooltip("Hoverboard's current speed")]
    public float speed;

    [Header("Drive Settings")]
    [Tooltip("How quickly the board picks up speed")]
    public float Acceleration = 10f;
    [Tooltip("How quickly the board loses speed after letting go of the acceleration key")]
    public float SlowingForce = .99f;
    [Tooltip("How quickly the board slows while breaking")]
    public float BrakingForce = .95f;
    [Tooltip("The angel the board shifts while making turns")]
    public float BoardBanking = 30f;

    [Header("Hover Settings")]
    [Tooltip("How high off the ground the board floats")]
    public float HoverHeight = 1f;
    [Tooltip("How high the board has to be before being considered falling")]
    public float FallDist = 5f;
    [Tooltip("The force pushing the board upward")]
    public float HoverForce = 20f;
    [Tooltip("Everything considered ground")]
    public LayerMask Ground;
    [Tooltip("The PID controller")]
    public PIDController PID;

    [Header("Physics Settings")]
    [Tooltip("The board's base")]
    public Transform BoardBody;
    [Tooltip("The max speed the board can go")]
    public float MaxSpeed = 100f;
    [Tooltip("Gravity when the board is on the gorund")]
    public float HoverGravity = 20f;
    [Tooltip("Gravity when the board is falling")]
    public float FallGravity = 80f;

    [Tooltip("Board's rigibody")]
    Rigidbody rigidbody;
    HoverboardInput Input;
    [Tooltip("The air resistasnce when moving forward")]
    float drag;
    [Tooltip("Is the ship on the ground?")]
    bool Grounded;



    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<HoverboardInput>();

        drag = Acceleration / MaxSpeed;
    }

    private void FixedUpdate()
    {
        speed = Vector3.Dot(rigidbody.velocity, transform.forward);

        CalculateHover();
        CalculatePropulsion();
    }

    private void CalculateHover()
    {
        Vector3 GroundNormal;

        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit Hit;

        Grounded = Physics.Raycast(ray, out Hit, Ground);

        if (Grounded)
        {
            float height = Hit.distance;
            GroundNormal = Hit.normal.normalized;
            float forcePercent = PID.Seek(HoverHeight, height);

            Vector3 force = GroundNormal * HoverForce * forcePercent;
            Vector3 gravity = -GroundNormal * HoverForce * height;

            rigidbody.AddForce(force, ForceMode.Acceleration);
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
        else
        {
            GroundNormal = Vector3.up;

            Vector3 gravity = -GroundNormal * FallGravity;
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }

        Vector3 projection = Vector3.ProjectOnPlane(transform.forward, GroundNormal);
        Quaternion rotation = Quaternion.LookRotation(projection, GroundNormal);

        rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, rotation, Time.deltaTime * 10f));

        float angle = BoardBanking * -Input.rudder;

        Quaternion BodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
        BoardBody.rotation = Quaternion.Lerp(BoardBody.rotation, BodyRotation, Time.deltaTime * 10f);

    }

    private void CalculatePropulsion()
    {
        float RotationTorque = Input.rudder - rigidbody.angularVelocity.y;

        rigidbody.AddRelativeTorque(0f, RotationTorque, 0f, ForceMode.VelocityChange);

        float SidewaysSpeed = Vector3.Dot(rigidbody.velocity, transform.right);

        Vector3 SideFriciton = -transform.right * (SidewaysSpeed / Time.fixedDeltaTime);

        rigidbody.AddForce(SideFriciton, ForceMode.Acceleration);

        if(Input.thruster <= 0f)
        {
            rigidbody.velocity *= BrakingForce;
        }

        if (!Grounded)
        {
            return;
        }

        if (Input.isBraking)
        {
            rigidbody.velocity *= BrakingForce;
        }

        float Propulsion = Acceleration * Input.thruster - drag * Mathf.Clamp(speed, 0f, MaxSpeed);
        rigidbody.AddForce(transform.forward * Propulsion, ForceMode.Acceleration);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Vector3 UpwardForceFromCollision = Vector3.Dot(collision.impulse, transform.up) * transform.up;
            rigidbody.AddForce(-UpwardForceFromCollision, ForceMode.Impulse);
        }
    }

    public float GetSpeedPercentage()
    {
        return rigidbody.velocity.magnitude / MaxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Rail End"))
        {
            this.GetComponent<HoverboardInput>().enabled = true;
        }
    }
}