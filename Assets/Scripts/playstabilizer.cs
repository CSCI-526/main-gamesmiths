using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StablePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float uprightStability = 10f;
    public float uprightSpeed = 10f;

    private Rigidbody rb;
    private Vector3 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Helps keep the player from rotating unintentionally
    }

    void Update()
    {
        // Get movement input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDirection = new Vector3(h, 0, v).normalized;
    }

    void FixedUpdate()
    {
        // Move the player
        Vector3 moveVelocity = inputDirection * moveSpeed;
        Vector3 velocity = rb.velocity;
        velocity.x = moveVelocity.x;
        velocity.z = moveVelocity.z;
        rb.velocity = velocity;

        // Face the direction of movement
        if (inputDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            Quaternion smoothRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }

        // Keep the player upright
        Quaternion currentRotation = transform.rotation;
        Quaternion uprightRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * currentRotation;
        transform.rotation = Quaternion.Slerp(currentRotation, uprightRotation, uprightSpeed * Time.fixedDeltaTime);
    }
}
