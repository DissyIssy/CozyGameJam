using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private CharacterController characterController;

    private Vector2 currentMovementInput;
    private Vector3 currentMovementDirection;
    
    private bool isMovementPressed;
    private bool isRunPressed;

    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;
    [SerializeField] private float rotationFactorPerFrame = 250f;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();

        playerInputActions.CharacterControls.Move.started += onMovementInput;
        playerInputActions.CharacterControls.Move.canceled += onMovementInput;
        playerInputActions.CharacterControls.Move.performed += onMovementInput;
        
        playerInputActions.CharacterControls.Run.started += onRun;
        playerInputActions.CharacterControls.Run.canceled += onRun;
    }

    private void Update()
    {
        float currentSpeed;
        handleRotation();
        
        if (isRunPressed)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
        }
        
        Vector3 movement = currentMovementDirection * currentSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    private void OnEnable()
    {
        playerInputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.CharacterControls.Disable();
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        
        positionToLookAt.x = currentMovementDirection.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovementDirection.z;
        
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            if (currentMovementDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
            }

        }
    }

    //handler function to set the player input values
    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        
        currentMovementDirection.x = currentMovementInput.x;
        currentMovementDirection.y = 0f;
        currentMovementDirection.z = currentMovementInput.y;

        if (currentMovementInput.x != 0 || currentMovementInput.y != 0)
        {
            isMovementPressed = true;
        }
        else
        {
            isMovementPressed = false;
        }
    }
}

