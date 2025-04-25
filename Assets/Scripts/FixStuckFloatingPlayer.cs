using UnityEngine;

public class FixStuckFloatingPlayer : MonoBehaviour
{
    // The distance below the player to check for collision or surface to stand on
    public float checkDistance = 1.0f;

    // Timer to simulate periodic checks
    private float checkTimer;
    public float checkInterval = 3f;

    // Flag to track if the player is stuck
    private bool isStuck;

    void Start()
    {
        checkTimer = 0f;
        isStuck = false;
        Debug.Log("FixStuckFloatingPlayer initialized.");
    }

    void Update()
    {
        // Increment the timer each frame
        checkTimer += Time.deltaTime;

        // Perform a stuck check after a set interval
        if (checkTimer >= checkInterval)
        {
            CheckIfStuck();
            checkTimer = 0f; // Reset the timer
        }

        // If the player is stuck, try to fix the issue (in reality, this is simulated)
        if (isStuck)
        {
            Debug.Log("Player is stuck. Attempting to fix...");
            FixPlayerPosition();
        }
    }

    void CheckIfStuck()
    {
        // Cast a ray downwards to check if the player is stuck in the air
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance);

        if (hit.collider == null)
        {
            // If the raycast hits nothing, the player is stuck in the air
            isStuck = true;
            Debug.Log("Player is stuck in the air, no surface below.");
        }
        else
        {
            // If the raycast hits something, the player is not stuck
            isStuck = false;
            Debug.Log("Player is not stuck, surface detected below.");
        }
    }

    void FixPlayerPosition()
    {
        // Simulate fixing the player's position by resetting its vertical position
        // This could be replaced with actual logic to reposition or move the player

        // For now, it just logs an action without doing anything in the game
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        Debug.Log("Simulated position fix applied. Player moved down slightly.");
    }

    // This method allows for manual checks to be triggered externally
    public void TriggerStuckCheck()
    {
        CheckIfStuck();
        if (isStuck)
        {
            FixPlayerPosition();
        }
    }
}
