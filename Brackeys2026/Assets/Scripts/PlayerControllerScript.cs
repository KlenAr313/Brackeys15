using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : EntityControllerScript
{
	[SerializeField] private InputAction playerMove;
	[SerializeField] private InputAction playerJump;

	private void OnEnable()
	{
		playerMove.Enable();
		playerJump.Enable();
	}

	private void OnDisable()
	{
		playerMove.Disable();
		playerJump.Disable();
	}

	void Update()
	{
		direction = Vector2Rotate(playerMove.ReadValue<Vector2>(), transform.localEulerAngles.y);
		jump = playerJump.IsPressed();
	}
}