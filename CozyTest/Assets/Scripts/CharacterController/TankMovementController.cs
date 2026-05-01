using System;
using UnityEngine;

public class TankMovementController : MonoBehaviour
{
    //Moves like a tank, decide a direction and then move forward
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 180f;
    
    private CharacterController controller;
    private PlayerInput playerInput;
    
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.CharacterControls.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
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
