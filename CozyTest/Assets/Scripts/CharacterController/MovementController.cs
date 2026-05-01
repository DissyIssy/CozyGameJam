using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;

    private Vector2 currentMovementInput;
    private Vector3 currentMovementDirection;
    
    private bool isMovementPressed;
    private bool isRunPressed;

    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runSpeed = 4f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    private void Update()
    {
        float currentSpeed;
        
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
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
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

