using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CharacterController characterController;
    [SerializeField] float velocity;
    [SerializeField] float rotationFactorPerFrame = 1f;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementStarted;
        playerInput.CharacterControls.Move.canceled += OnMovementCanceled;
        playerInput.CharacterControls.Move.performed += OnMovementPerformed;
    }

    void HandleMovement()
    {
        if (isMovementPressed)
        {
            // Get the camera's forward direction
            Vector3 cameraForward = Camera.main.transform.forward;
            // Ignore the y-axis component to keep the direction in the horizontal plane
            cameraForward.y = 0f;
            // Use the camera's forward direction for movement
            currentMovement = cameraForward.normalized * currentMovementInput.y + Camera.main.transform.right.normalized * currentMovementInput.x;
        }
        else
        {
            // If no movement input, stop the player
            currentMovement = Vector3.zero;
        }
    }

    void HandleRotation()
    {
        if (isMovementPressed && currentMovement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(currentMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        characterController.Move((currentMovement * velocity) * Time.deltaTime);
    }

    void OnMovementStarted(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = true;
    }

    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    void OnMovementCanceled(InputAction.CallbackContext context)
    {
        currentMovementInput = Vector2.zero;
        isMovementPressed = false;
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
