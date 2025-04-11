using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AdvancedPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float velocityPower = 0.9f;

    [Header("Jump Settings")]
    public float jumpForce = 16f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public float gravityScale = 3f;
    public float fallMultiplier = 2.5f;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float inputX;
    private bool isGrounded;
    private float lastGroundedTime;
    private float lastJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        // Track grounded time for coyote jump
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded) lastGroundedTime = Time.time;

        // Jump input buffering
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = Time.time;
        }

        // Jump logic
        if (Time.time - lastJumpTime <= jumpBufferTime && Time.time - lastGroundedTime <= coyoteTime)
        {
            Jump();
            lastJumpTime = 0;
            lastGroundedTime = 0;
        }

        BetterJumpPhysics();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float targetSpeed = inputX * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velocityPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void BetterJumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = gravityScale * 1.5f;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
