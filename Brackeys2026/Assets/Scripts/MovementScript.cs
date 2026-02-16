using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private EntityControllerScript controller;

	private new Rigidbody rigidbody;
	protected bool canJump;

	public IEnumerator ResetJump()
	{
		yield return new WaitForSeconds(3);

		canJump = true;
	}

	void Start()
	{
		rigidbody = gameObject.GetComponent<Rigidbody>();

		canJump = true;
	}

	void FixedUpdate()
	{
		Vector2 force = controller.MovementSpeed * Time.fixedDeltaTime * controller.Direction;
		rigidbody.linearVelocity = new Vector3(force.x, rigidbody.linearVelocity.y, force.y);


		if (controller.Jump && canJump)
		{
			rigidbody.linearVelocity += new Vector3(0, controller.JumpForce * Time.fixedDeltaTime, 0);
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
}