using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : EntityControllerScript
{
	[SerializeField] private float sensitivity;
	[SerializeField] private InputAction mouseMove;
	[SerializeField] private InputAction horizontalDirections;
	[SerializeField] private InputAction verticalDirection;

	private Vector2 pervMousePosition = Vector2.zero;

	private void OnEnable()
	{
		mouseMove.Enable();
		horizontalDirections.Enable();
		verticalDirection.Enable();
	}

	private void OnDisable()
	{
		mouseMove.Disable();
		horizontalDirections.Disable();
		verticalDirection.Disable();
	}

	private void Update()
	{
		direction = Vector2Rotate(horizontalDirections.ReadValue<Vector2>(), -transform.localEulerAngles.y);
		jump = verticalDirection.IsPressed();
	}

	private void FixedUpdate()
	{
		Vector2 mouseMovement = mouseMove.ReadValue<Vector2>();

		transform.localEulerAngles += sensitivity * new Vector3(mouseMovement.y / 4, mouseMovement.x, 0);
	}
}