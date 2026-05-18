using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -20f;

    [Header("References")]
    public Transform playerOrientation;
    public Transform model;
    public Animator animator;

    [Header("Input")]
    public InputActionReference moveAction;

    private CharacterController characterController;
    private Vector3 movementDirection;
    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null && moveAction.action != null)
            moveAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null && moveAction.action != null)
            moveAction.action.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    private void HandleMovement()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (characterController == null || !characterController.enabled || !characterController.gameObject.activeInHierarchy)
        {
            movementDirection = Vector3.zero;
            return;
        }

        Vector2 input = Vector2.zero;

        if (moveAction != null && moveAction.action != null)
            input = moveAction.action.ReadValue<Vector2>();

        Transform orientation = playerOrientation != null ? playerOrientation : transform;
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        movementDirection =
            forward * input.y +
            right * input.x;

        if (movementDirection.sqrMagnitude > 1f)
            movementDirection.Normalize();

        if (characterController.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        Vector3 finalMove = movementDirection * movementSpeed;
        finalMove.y = verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);

        bool isMovingBackward = input.y < -0.01f;
        if (movementDirection.magnitude > 0.1f && model != null && !isMovingBackward)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            model.rotation = Quaternion.Slerp(
                model.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void HandleAnimation()
    {
        bool isWalking = movementDirection.magnitude > 0.1f;

        if (animator != null)
            animator.SetBool("isWalking", isWalking);
    }
}
