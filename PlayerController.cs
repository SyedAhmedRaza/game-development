using UnityEngine;

/// <summary>
/// A basic Third-Person Character Controller for an open-world game prototype.
/// Attach this to your Player GameObject.
/// Requires a CharacterController component and a Camera reference.
/// </summary>
public class SimpleThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 6.0f;
    public float runSpeed = 12.0f;
    public float rotationSpeed = 10.0f;
    public float gravity = -9.81f;

    [Header("Camera References")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component missing on this object!");
        }
        
        if (cameraTransform == null)
        {
            // Fallback to main camera if not assigned
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Check if grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Get Input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calculate direction relative to camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        
        // Flatten vectors to prevent flying when looking up/down
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Move the character
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        
        if (moveDirection.magnitude >= 0.1f)
        {
            // Rotate character to face movement direction
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
