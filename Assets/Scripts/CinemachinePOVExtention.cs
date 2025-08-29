using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 80f;

    private INPUTMANAGER inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        inputManager = INPUTMANAGER.instance;  // Use public Instance property, correct casing
        base.Awake();
    }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (vcam.Follow != null && stage == CinemachineCore.Stage.Aim)
        {
            if (startingRotation == Vector3.zero)
                startingRotation = transform.localRotation.eulerAngles;

            Vector2 deltaInput = inputManager.GetMouseDelta(); // Use your INPUTMANAGER method name here

            // Correct rotation application for natural mouse control:
            startingRotation.y += deltaInput.x * horizontalSpeed * deltaTime;   // yaw - horizontal mouse movement
            startingRotation.x -= deltaInput.y * verticalSpeed * deltaTime;     // pitch - vertical mouse movement (inverted)

            startingRotation.x = Mathf.Clamp(startingRotation.x, -clampAngle, clampAngle);

            state.RawOrientation = Quaternion.Euler(startingRotation.x, startingRotation.y, 0f);
        }
    }
}
