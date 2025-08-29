using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 5.0f;

    [Header("Input Actions")]
    public InputActionReference MovementAction; // expects Vector2
    public InputActionReference LookAction; // expects Vector2

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        if (MovementAction != null && MovementAction.action != null)
            MovementAction.action.Enable();
        else
            Debug.LogWarning("MovementAction or its action is not assigned.");

        if (LookAction != null && LookAction.action != null)
            LookAction.action.Enable();
        else
            Debug.LogWarning("LookAction or its action is not assigned.");
    }

    void OnDisable()
    {
        if (MovementAction != null && MovementAction.action != null)
            MovementAction.action.Disable();

        if (LookAction != null && LookAction.action != null)
            LookAction.action.Disable();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Read movement input
        Vector2 movement = Vector2.zero;
        if (MovementAction != null && MovementAction.action != null)
            movement = MovementAction.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        if (move != Vector3.zero && groundedPlayer)
        {
            transform.forward = move;
        }

        // Read look input if needed (example log)
        Vector2 look = Vector2.zero;
        if (LookAction != null && LookAction.action != null)
        {
            look = LookAction.action.ReadValue<Vector2>();
            // Use look input for camera or rotation logic here
            // Example: Debug.Log($"Look input: {look}");
        }

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
