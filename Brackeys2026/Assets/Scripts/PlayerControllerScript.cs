using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    private CharacterController controller;
    public float MovementSpeed = 5f;
    public float Gravity = 1f;
    public float JumpForce = 10;

    [SerializeField] private int damage = 10;
	[SerializeField] private int health = 100;
	private GameObject centerOfMass;
	public static PlayerControllerScript Instance;

	//protected bool canJump;

	private bool inWater;

    private float verticalVelocity = 0f;

     void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        controller = GetComponent<CharacterController>();

        centerOfMass = GameObject.Find("Camera");
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * MovementSpeed * Time.deltaTime;

        if (inWater)
		{
			move = move * 0.4f;
		}

        controller.Move(move);

        verticalVelocity = verticalVelocity + Gravity * Time.deltaTime;
        controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    public void Jump()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = JumpForce;
        }
    }


    private void OnCollisionEnter(Collision collision)
	{
		//if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			//canJump = true;
		
		if (collision.gameObject.tag == "Water")
		{
			inWater = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		//if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
			//canJump = false;

		if (collision.gameObject.tag == "Water")
		{
			inWater = false;
		}
	}

	public void Punch()
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
