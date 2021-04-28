using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WallWalker : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float MovementSpeed = 10.0f;
	[SerializeField] private float MaxVelocityChange = 14.0f;
	[Tooltip("How smooth the player rotates when latching to a surface")] [SerializeField] private float LerpSpeed = 5;
	public bool CanWallWalk = true;

	[Header("Jump")]
	[SerializeField] private float JumpHeight = 8;
	[SerializeField] private bool Grounded = false;
	private bool DoJump;

	[Header("Components")]
	private Rigidbody rigidbody;

	[Header("Gravity")]
	[SerializeField] private float Gravity = 10;
	[Tooltip("How close the player's feet have to be to the surface to lock onto it")] [SerializeField] private float GravityLock = 1.3f;
	[SerializeField] private LayerMask Walkble;
	private Vector3 SurfaceNormal;
	private Vector3 MyNormal;
	private float GroundDistance;
	private float DeltaGround = .2f;
	private bool IsGrounded;

	[Header("Player Step")]
	[SerializeField] private Transform StepRayUpper;
	[SerializeField] private Transform StepRayLower;
	[SerializeField] private float StepHeight = 0.3f;
	[SerializeField] private float StepSmooth = 0.1f;
 
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();

		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;

		StepRayUpper.position = new Vector3(StepRayUpper.position.x, StepHeight, StepRayUpper.position.z);
	}

	void FixedUpdate()
	{
		if (DoJump)
		{
			rigidbody.AddForce(transform.up * JumpHeight, ForceMode.VelocityChange);
			DoJump = false;
		}

		// Calculate how fast we should be moving
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= MovementSpeed;

		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);

		if (!Grounded)
		{
			velocityChange.y = 0;
		}
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

		// When not grounded, use the rigibody's built in gravity
		if (!Grounded)
		{
			rigidbody.useGravity = true;
		}
		else
		{
			rigidbody.useGravity = false;
		}

		StepClimb();
	}

	private void Update()
	{
		if (Grounded)
		{
			// Jump
			if (Input.GetKeyDown(KeyCode.Space))
			{
				DoJump = true;
				//rigidbody.AddForce(transform.up * JumpHeight, ForceMode.VelocityChange);
			}
		}

		Debug.DrawLine(transform.position, transform.position - transform.up * 1.25f, Color.red);
		if (Physics.Raycast(transform.position, -transform.up, 1.25f, Walkble))
		{
			Grounded = true;
		}
		else
		{
			Grounded = false;
		}

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

		if (!Grounded)
		{
			Vector3 NewRotaton = transform.localEulerAngles;
			Quaternion ResetRotation = Quaternion.Euler(NewRotaton);
			transform.rotation = Quaternion.Lerp(transform.rotation, ResetRotation, 1);
		}
	}

	private void StepClimb()
    {
		RaycastHit HitLower;
		if(Physics.Raycast(StepRayLower.position, transform.TransformDirection(Vector3.forward), out HitLower, 0.1f))
        {
			RaycastHit HitUpper;
			if (!Physics.Raycast(StepRayUpper.position, transform.TransformDirection(Vector3.forward), out HitUpper, 0.2f))
			{
				rigidbody.position -= new Vector3(0f, -StepSmooth, 0f);
			}
		}

		RaycastHit HitLower45;
		if (Physics.Raycast(StepRayLower.position, transform.TransformDirection(1.5f,0,1), out HitLower45, 0.1f))
		{
			RaycastHit HitUpper45;
			if (!Physics.Raycast(StepRayUpper.position, transform.TransformDirection(1.5f,0,1), out HitUpper45, 0.2f))
			{
				rigidbody.position -= new Vector3(0f, -StepSmooth, 0f);
			}
		}

		RaycastHit HitLowerMinus45;
		if (Physics.Raycast(StepRayLower.position, transform.TransformDirection(-1.5f, 0, 1), out HitLowerMinus45, 0.1f))
		{
			RaycastHit HitUpperMinus45;
			if (!Physics.Raycast(StepRayUpper.position, transform.TransformDirection(-1.5f, 0, 1), out HitUpperMinus45, 0.2f))
			{
				rigidbody.position -= new Vector3(0f, -StepSmooth, 0f);
			}
		}
	}
}
