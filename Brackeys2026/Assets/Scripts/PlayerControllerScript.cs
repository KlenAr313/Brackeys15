using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : EntityControllerScript
{
	[SerializeField] private float sensitivity;
	[SerializeField] private InputAction horizontalDirections;
	[SerializeField] private InputAction verticalDirection;

	private float xRotation;
	private float yRotation;

	private void OnEnable()
	{
		horizontalDirections.Enable();
		verticalDirection.Enable();
	}

	private void OnDisable()
	{
		horizontalDirections.Disable();
		verticalDirection.Disable();
	}

	private void Update()
	{
		direction = Vector2Rotate(horizontalDirections.ReadValue<Vector2>(), -transform.localEulerAngles.y);
		jump = verticalDirection.IsPressed();

		Vector2 mouseMovement = Mouse.current.delta.ReadValue();

		yRotation += mouseMovement.x * sensitivity;
		xRotation -= mouseMovement.y * sensitivity;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);
	}
}