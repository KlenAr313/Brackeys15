using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : EntityControllerScript
{
	[SerializeField] private float sensitivity;
	private float originalSensitivity;
	private InputAction horizontalDirections;
	[SerializeField] private InputAction verticalDirection;
	[SerializeField] private InputAction horizontalSpeed;

	[SerializeField] private InputAction punchAction;

	private Transform cameraParentTransform;

	private float xRotation;
	private float yRotation;

    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
    public float OriginalSensitivity { get => originalSensitivity; set => originalSensitivity = value; }

    private new void Start()
    {
		base.Start();
		horizontalDirections = InputSystem.actions.FindAction("Move");
		OriginalSensitivity = sensitivity;
		cameraParentTransform = GameObject.Find("Camera").transform;

		horizontalDirections.Enable();
		verticalDirection.Enable();
		punchAction.Enable();
	}

    private void OnEnable()
	{
		if(horizontalDirections != null)
		{
			horizontalDirections.Enable();
		}
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
		direction = Vector2Rotate(horizontalDirections.ReadValue<Vector2>(), -cameraParentTransform.localEulerAngles.y);
		if(jump = verticalDirection.WasPressedThisFrame())
		{
			PlayerControllerScript.Instance.Jump();
		}

		Vector2 mouseMovement = Mouse.current.delta.ReadValue();

		yRotation += mouseMovement.x * Sensitivity;
		xRotation -= mouseMovement.y * Sensitivity;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		cameraParentTransform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);

		PlayerControllerScript.Instance.Move(direction);


		if(punchAction.triggered)
		{
			PlayerControllerScript.Instance.Punch();
		}
	}

}