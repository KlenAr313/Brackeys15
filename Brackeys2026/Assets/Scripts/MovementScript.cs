using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private EntityControllerScript controller;
	private GameObject gameManager;

	private new Rigidbody rigidbody;
	protected bool canJump;

	private void Start()
	{
		rigidbody = gameObject.GetComponent<Rigidbody>();
		gameManager = GameObject.Find("GameManager");

		canJump = true;
	}

	private void FixedUpdate()
	{
		Vector2 force = controller.MovementSpeed * Time.fixedDeltaTime * controller.Direction;
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
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			canJump = false;
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