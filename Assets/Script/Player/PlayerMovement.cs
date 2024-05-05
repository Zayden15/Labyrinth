using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;
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
       
            Vector3 cameraForward = Camera.main.transform.forward;
           
            cameraForward.y = 0f;
            
            currentMovement = cameraForward.normalized * currentMovementInput.y + Camera.main.transform.right.normalized * currentMovementInput.x;
        }
        else
        {
            
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

    public void HandleAnimation() {

        bool isRunning = animator.GetBool("isRunning");
        bool isPushing = animator.GetBool("isPushing");

        if (isMovementPressed && !isRunning)
        {

            animator.SetBool("isRunning", true);
        }

        else if (!isMovementPressed && isRunning) {
            animator.SetBool("isRunning", false);
        }

        /*if (Input.GetKeyDown(KeyCode.E)) {
            animator.SetBool("isPushing", true);
        }*/
    }

    void Update()
    {
        HandleAnimation();
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
