using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript : EntityControllerScript
{
	[SerializeField] private float sensitivity;
	private float originalSensitivity;
	[SerializeField] private InputAction horizontalDirections;
	[SerializeField] private InputAction verticalDirection;
	[SerializeField] private InputAction horizontalSpeed;

	[SerializeField] private InputAction punchAction;

	private float xRotation;
	private float yRotation;

    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
    public float OriginalSensitivity { get => originalSensitivity; set => originalSensitivity = value; }

#pragma warning disable CS0108
    private void Start()
    {
		OriginalSensitivity = sensitivity;
	}
#pragma warning restore CS0108 
    private void OnEnable()
	{
		horizontalDirections.Enable();
		verticalDirection.Enable();
		punchAction.Enable();
	}

	private void OnDisable()
	{
		horizontalDirections.Disable();
		verticalDirection.Disable();
		punchAction.Disable();
	}

	private void Update()
	{
		direction = Vector2Rotate(horizontalDirections.ReadValue<Vector2>(), -transform.localEulerAngles.y);
		jump = verticalDirection.IsPressed();

		Vector2 mouseMovement = Mouse.current.delta.ReadValue();

		yRotation += mouseMovement.x * Sensitivity;
		xRotation -= mouseMovement.y * Sensitivity;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);

		if(punchAction.triggered)
		{
			punch = true;
		}
	}

}