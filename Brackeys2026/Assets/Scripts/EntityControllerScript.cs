using UnityEngine;

public abstract class EntityControllerScript : MonoBehaviour
{
	[SerializeField] protected float movementSpeed = 50;
	[SerializeField] protected float jumpForce = 50;

	protected new Rigidbody rigidbody;
	protected Vector2 direction;
	protected bool jump;
	protected bool punch;

	public float MovementSpeed => movementSpeed;
	public float JumpForce => jumpForce;
	public Vector2 Direction => direction;
	public bool Jump => jump;
	public bool Punch { get => punch; set => punch = value; }

	protected Vector2 Vector2Rotate(Vector2 vector, float angle)
	{
		float radians = Mathf.Deg2Rad * angle;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
	}

	protected void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = true;
	}
}
