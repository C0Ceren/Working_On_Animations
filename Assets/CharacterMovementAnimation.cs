using UnityEngine;

using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerWithAnimation : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;
    private bool isRunPressed;

    [SerializeField] private float acceleration = 2.0f;
    [SerializeField] private float deceleration = 2.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float rotationFactorPerFrame = 10.0f;

    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimation();
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void HandleMovement()
    {
        float targetSpeed = isRunPressed ? runSpeed : walkSpeed;

        if (isMovementPressed)
        {
            velocityX = Mathf.MoveTowards(velocityX, currentMovement.x * targetSpeed, acceleration * Time.deltaTime);
            velocityZ = Mathf.MoveTowards(velocityZ, currentMovement.z * targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            velocityX = Mathf.MoveTowards(velocityX, 0, deceleration * Time.deltaTime);
            velocityZ = Mathf.MoveTowards(velocityZ, 0, deceleration * Time.deltaTime);
        }

        Vector3 move = new Vector3(velocityX, 0, velocityZ);
        characterController.Move(move * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (isMovementPressed)
        {
            Vector3 positionToLookAt = new Vector3(currentMovement.x, 0.0f, currentMovement.z);
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void HandleAnimation()
    {
        animator.SetFloat("VelocityX", velocityX);
        animator.SetFloat("VelocityZ", velocityZ);
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
