using System;
using UnityEngine;

public class TankMovementController : MonoBehaviour
{
    //Moves like a tank, decide a direction and then move forward
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float gravity = -9f;
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float runMultiplier = 1.5f;

    private bool isRunning;
    private CharacterController controller;
    private PlayerInputActions playerInputActions;
    private float verticalVelocity;
    
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputActions = new PlayerInputActions();

        playerInputActions.CharacterControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputActions.CharacterControls.Move.canceled += ctx => moveInput = Vector2.zero;
        playerInputActions.CharacterControls.Jump.performed += ctx => Jump();
        playerInputActions.CharacterControls.Run.performed += ctx => isRunning = true;
        playerInputActions.CharacterControls.Run.canceled += ctx => isRunning = false;
    }

    private void OnEnable()
    {
        playerInputActions.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.CharacterControls.Disable();
    }

    private void Update()
    {
        Vector3 movDir;
        
        //Rotate
        transform.Rotate(0, moveInput.x * turnSpeed * Time.deltaTime, 0);

        //Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        //Running
        float currentSpeed;
        if (isRunning)
        {
            currentSpeed = speed * runMultiplier;
        }
        else
        {
            currentSpeed = speed;
        }
        
        //Move
        movDir = transform.forward * moveInput.y * currentSpeed;
        movDir.y = verticalVelocity;
        controller.Move(movDir * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic)
            return;

        float currentPushForce;

        if (isRunning)
        {
            currentPushForce = pushForce * 2f;
        }
        else
        {
            currentPushForce = pushForce;
        }
        
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.AddForce(pushDirection * currentPushForce, ForceMode.Force);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
