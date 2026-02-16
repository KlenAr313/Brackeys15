using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private EntityControllerScript controller;

	private Rigidbody rb;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		Vector2 force = controller.Speed * Time.fixedDeltaTime * controller.Direction;
		rb.linearVelocity = new Vector3(force.x, 0, force.y);
	}
}