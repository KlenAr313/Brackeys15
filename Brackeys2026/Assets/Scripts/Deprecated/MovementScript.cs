using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private EntityControllerScript controller;
	[SerializeField] private int damage = 10;
	[SerializeField] private int health = 100;
	private GameObject centerOfMass;
	[SerializeField] private float speedMody;

	public static MovementScript Instance;

	private new Rigidbody rigidbody;
	protected bool canJump;
	private Vector2 force;

	private bool inWater;

	private void Start()
	{

		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			return;
		}

		rigidbody = gameObject.GetComponent<Rigidbody>();
		centerOfMass = GameObject.Find("Camera");

		canJump = true;
		inWater = false;
	}

	private void FixedUpdate()
	{
		
		force = controller.MovementSpeed * controller.Direction;
		if (inWater)
		{
			force *= speedMody;
		}
		//rigidbody.linearVelocity = new Vector3(force.x, rigidbody.linearVelocity.y, force.y);
		rigidbody.AddForce(new Vector3(force.x, 0, force.y));

		if (controller.Jump && canJump)
			rigidbody.AddForce(new Vector3(0, controller.JumpForce, 0));

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

		if (Physics.Raycast(centerOfMass.transform.position, centerOfMass.transform.forward, out RaycastHit hit, 100f))
        {
			Debug.Log("Hit: " + hit.collider.name);
			if(hit.collider.tag.ToLower() == "enemy")
			{
				EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

				enemy.TakeDamage(damage);
			}
        }
	}

	public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
		Debug.Log("Player Health: " + health);
    }

    private void Die()
    {
		//TODO: Add death animation and game over system
        Debug.Log("You Died!");
    }
}