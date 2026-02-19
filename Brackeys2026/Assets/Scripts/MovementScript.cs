using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private EntityControllerScript controller;
	[SerializeField] private float speedMody;

	private GameObject gameManager;

	private new Rigidbody rigidbody;
	protected bool canJump;
	private Vector2 force;

	private bool inWater;


	private void Start()
	{
		rigidbody = gameObject.GetComponent<Rigidbody>();

		canJump = true;
		inWater = false;
	}

	private void FixedUpdate()
	{
		
		force = controller.MovementSpeed * Time.fixedDeltaTime * controller.Direction;
		if (inWater)
		{
			force = force * 0.4f;
		}
		rigidbody.linearVelocity = new Vector3(force.x, rigidbody.linearVelocity.y, force.y);

		


		if (controller.Jump && canJump)
			rigidbody.linearVelocity += new Vector3(0, controller.JumpForce * Time.fixedDeltaTime, 0);

		if (controller.Punch)
		{
			Punch();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			canJump = true;
		
		if (collision.gameObject.tag == "Water")
		{
			inWater = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			canJump = false;

		if (collision.gameObject.tag == "Water")
		{
			inWater = false;
		}
	}

	private void Punch()
	{
		CustomAnimator[] Animations = GameObject.Find("Fists").GetComponents<CustomAnimator>();

		CustomAnimator punchAnimator = null;
		foreach (CustomAnimator anim in Animations)
		{
			if (anim.animationName == "punch")
			{
				punchAnimator = anim;
				break;
			}
		}

		if (punchAnimator == null || punchAnimator.IsPlaying)
		{
			return;
		}

		controller.Punch = false;
		punchAnimator.PlayAnimation();
	}
}