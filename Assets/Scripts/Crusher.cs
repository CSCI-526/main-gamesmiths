using UnityEngine;

public class GameController : MonoBehaviour
{
    // === PLAYER CONTROLS ===
    public GameObject player;
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    private float moveSpeed;
    private Rigidbody playerRb;

    // === OBSTACLE CONTROLS ===
    public GameObject movingObstacle;
    public Vector3 obstaclePointA;
    public Vector3 obstaclePointB;
    public float obstacleSpeed = 10f;
    private bool goingToB = true;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        moveSpeed = normalSpeed;
    }

    void Update()
    {
        HandlePlayerMovement();
        HandleObstacleMovement();
    }

    // === PLAYER MOVEMENT ===
    void HandlePlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed;
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y, movement.z);
    }

    // === OBSTACLE MOVEMENT ===
    void HandleObstacleMovement()
    {
        if (movingObstacle == null) return;

        Vector3 target = goingToB ? obstaclePointB : obstaclePointA;
        movingObstacle.transform.position = Vector3.MoveTowards(movingObstacle.transform.position, target, obstacleSpeed * Time.deltaTime);

        if (Vector3.Distance(movingObstacle.transform.position, target) < 0.1f)
        {
            goingToB = !goingToB;
        }
    }

    // === TRIGGER EVENTS ===
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlowZone"))
        {
            moveSpeed = slowSpeed;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlowZone"))
        {
            moveSpeed = normalSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == movingObstacle && collision.collider.CompareTag("Player"))
        {
            Vector3 pushDir = (collision.transform.position - movingObstacle.transform.position).normalized;
            playerRb.AddForce(pushDir * 300f);
        }
    }
}
