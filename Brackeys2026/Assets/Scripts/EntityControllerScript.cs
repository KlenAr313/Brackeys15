using UnityEngine;

public abstract class EntityControllerScript : MonoBehaviour
{
	[SerializeField] protected float speed = 50;

	protected Vector2 direction;
	protected Transform tr;

	public float Speed => speed;
	public Vector2 Direction => direction;

	protected Vector2 Vector2Rotate(Vector2 vector, float angle)
	{
		float radians = Mathf.Deg2Rad * angle;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);

		return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
	}

	protected void Start()
	{
		Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		tr = gameObject.GetComponent<Transform>();
	}
}
