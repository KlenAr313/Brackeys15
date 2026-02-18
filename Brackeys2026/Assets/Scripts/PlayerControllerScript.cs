using Unity.IO.LowLevel.Unsafe;
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

	private Transform cameraParentTransform;

	private float xRotation;
	private float yRotation;

    public float Sensitivity { get => sensitivity; set => sensitivity = value; }
    public float OriginalSensitivity { get => originalSensitivity; set => originalSensitivity = value; }

    private new void Start()
    {
		base.Start();
		OriginalSensitivity = sensitivity;
		cameraParentTransform = GameObject.Find("Camera").transform;
	}

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
		direction = Vector2Rotate(horizontalDirections.ReadValue<Vector2>(), -cameraParentTransform.localEulerAngles.y);
		jump = verticalDirection.IsPressed();

		Vector2 mouseMovement = Mouse.current.delta.ReadValue();

		yRotation += mouseMovement.x * Sensitivity;
		xRotation -= mouseMovement.y * Sensitivity;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		cameraParentTransform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);

		if(punchAction.triggered)
		{
			punch = true;
		}
	}

}