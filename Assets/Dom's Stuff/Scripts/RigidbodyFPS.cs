using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyFPS : MonoBehaviour
{

	[Header("Movement")]
	[SerializeField] private float MovementSpeed = 10.0f;
	[SerializeField] private float MaxVelocityChange = 10.0f;
	[Tooltip("How smooth the player rotates when latching to a surface")] [SerializeField] private float LerpSpeed = 5;
	public bool CanWallWalk = false;

	[Header("Jump")]
	private bool canJump = true;
	[SerializeField] private float JumpHeight = 2.0f;
	private bool Grounded = false;

	[Header("Components")]
	private Rigidbody rigidbody;

	[Header("Gravity")]
	[SerializeField] private float Gravity = 10.0f;
	[Tooltip("How close the player's feet have to be to the surface to lock onto it")] [SerializeField] private float GravityLock = 2;
	private Vector3 SurfaceNormal;
	private Vector3 MyNormal;
	private float GroundDistance;
	private float DeltaGround = .2f;
	private bool IsGrounded;

	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void FixedUpdate()
	{
		// Calculate how fast we should be moving
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= MovementSpeed;

		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
		velocityChange.y = 0;
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

		if (Grounded)
		{
			// Jump
			if (canJump && Input.GetKeyDown(KeyCode.Space))
			{
				rigidbody.AddForce(transform.up * JumpHeight, ForceMode.VelocityChange);
			}
		}

		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3(0, -Gravity * rigidbody.mass, 0));

		Grounded = false;
	}

	private void Update()
	{

		// Gravity
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

	void OnCollisionStay()
	{
		Grounded = true;
	}
}
