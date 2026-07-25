using UnityEngine;

/// <summary>
/// Basic Vehicle Controller for cars.
/// Implements simple arcade physics for driving mechanics.
/// Attach this to the root of your Car GameObject.
/// </summary>
public class SimpleCarController : MonoBehaviour
{
    [Header("Engine Settings")]
    public float acceleration = 15f;
    public float braking = 20f;
    public float maxSpeed = 60f;
    public float turnSpeed = 100f;

    [Header("Physics")]
    public float drag = 0.5f;

    private float currentSpeed;
    private float steeringInput;
    private float motorInput;
    
    // References (assign in Inspector)
    public Transform[] wheels; // Optional: for visual rotation

    void Update()
    {
        HandleInput();
        ApplyPhysics();
    }

    void HandleInput()
    {
        // Acceleration / Braking
        if (Input.GetKey(KeyCode.W))
        {
            motorInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            motorInput = -1f;
        }
        else
        {
            motorInput = 0f;
        }

        // Steering
        steeringInput = Input.GetAxis("Horizontal");
    }

    void ApplyPhysics()
    {
        // Calculate speed magnitude
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
        currentSpeed = localVelocity.z;

        // Apply Motor Force
        if (motorInput != 0)
        {
            if (Mathf.Abs(currentSpeed) < maxSpeed)
            {
                float force = motorInput * acceleration;
                // If braking (opposite direction), apply stronger force
                if ((motorInput > 0 && currentSpeed < 0) || (motorInput < 0 && currentSpeed > 0))
                {
                    force = motorInput * braking;
                }
                
                GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Acceleration);
            }
        }

        // Apply Drag (Air resistance / Friction)
        Vector3 dragVector = -GetComponent<Rigidbody>().velocity.normalized * drag;
        GetComponent<Rigidbody>().AddForce(dragVector, ForceMode.Acceleration);

        // Apply Steering (Only when moving)
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            float turnFactor = Mathf.Clamp(Mathf.Abs(currentSpeed) / 10f, 0.5f, 1f);
            float actualTurnSpeed = turnSpeed * turnFactor * Time.deltaTime;
            
            if (currentSpeed > 0)
                transform.Rotate(0, steeringInput * actualTurnSpeed, 0);
            else
                transform.Rotate(0, -steeringInput * actualTurnSpeed, 0); // Reverse steering when reversing
        }
        
        // Visual Wheel Rotation (Optional)
        RotateWheels();
    }

    void RotateWheels()
    {
        foreach (Transform wheel in wheels)
        {
            // Rotate based on speed
            wheel.Rotate(Vector3.right, currentSpeed * Time.deltaTime * 10f);
            
            // Steer front wheels (assuming first 2 are front)
            if (Array.IndexOf(wheels, wheel) < 2) 
            {
                Quaternion originalRotation = wheel.localRotation;
                wheel.localRotation = Quaternion.Euler(0, steeringInput * 30f, 0);
            }
        }
    }
}
