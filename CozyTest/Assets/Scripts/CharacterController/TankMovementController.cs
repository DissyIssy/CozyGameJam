using System;
using UnityEngine;

public class TankMovementController : MonoBehaviour
{
    //Moves like a tank, decide a direction and then move forward
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 180f;
    
    private CharacterController controller;
    private PlayerInputActions playerInputActions;
    
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInputActions = new PlayerInputActions();

        playerInputActions.CharacterControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInputActions.CharacterControls.Move.canceled += ctx => moveInput = Vector2.zero;
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

        //Move
        movDir = transform.forward * moveInput.y * speed;
        controller.Move(movDir * Time.deltaTime);
    }
}
