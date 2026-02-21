using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    public CharacterController controller;
    public float MovementSpeed = 5f;
    public float Gravity = 1f;
    public float JumpForce = 10;

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private int damage = 10;
	[SerializeField] private int health = 100;
    private int maxHealth;
	private GameObject centerOfMass;
	public static PlayerControllerScript Instance;

	//protected bool canJump;
    private bool isJumping = false;
    private bool isJumpingLastFrame = false;

	private bool inWater;

    [SerializeField]private float verticalVelocity = 0f;

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

        maxHealth = health;

        controller = GetComponent<CharacterController>();

        centerOfMass = GameObject.Find("Camera");
    }

    void Update()
    {
        isJumpingLastFrame = isJumping;
    }

    public void Move(Vector2 movementVector)
{
    Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
    move *= MovementSpeed * Time.deltaTime;

    if (inWater)
        move *= 0.4f;

    // Ground reset
    if (controller.isGrounded && verticalVelocity < 0)
    {
        verticalVelocity = -2f; // small downward force to keep grounded
    }

    // Apply gravity differently when falling
    if (verticalVelocity < 0)
    {
        // Falling
        verticalVelocity += Gravity * 2.5f * Time.deltaTime; // fall multiplier
    }
    else
    {
        // Rising
        verticalVelocity += Gravity * Time.deltaTime;
    }

    Vector3 verticalMove = new Vector3(0, verticalVelocity, 0) * Time.deltaTime;

    controller.Move(move + verticalMove);
}

    public void Jump()
    {
        if(controller.isGrounded)
        {
            isJumping = true;
            verticalVelocity = JumpForce;
        }
    }

    private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.tag.ToLower() == "spawnpoint")
		{
			spawnPoint = collision.gameObject;
		}
	}

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //canJump = true;
        }
		
		if (collision.gameObject.tag == "Water")
		{
			inWater = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //canJump = false;
        }

		if (collision.gameObject.tag == "Water")
		{
			inWater = false;
		}
	}

	public void Punch()
	{

        int rnd = Random.Range(0, 4);
        string fistName = null;
        
        switch (rnd)
        {
            case 0: fistName = "RFist"; break;
            case 1: fistName = "RFist"; break;
            case 2: fistName = "RFist"; break;
            case 3: fistName = "LFist"; break;
        };

        CustomAnimator[] Animations = GameObject.Find(fistName).GetComponents<CustomAnimator>();

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

		if (Physics.Raycast(centerOfMass.transform.position, centerOfMass.transform.forward, out RaycastHit hit, 1f))
        {
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
        HealthScript.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
		Debug.Log("Player Health: " + health);
    }

    private void Die()
    {
		DeathScript.Die();
    }

    public void Respawn()
    {
        controller.enabled = false;
        this.gameObject.transform.position = spawnPoint.transform.position;
        controller.enabled = true;
        verticalVelocity = 0f;
        health = maxHealth;
        HealthScript.SetHealth(health, maxHealth);

        foreach (EnemyBase enemy in SlotManager.Instance.Enemies)
        {
            if(enemy != null)
            {
                enemy.Reset();
            }
                
        }
    }
}
