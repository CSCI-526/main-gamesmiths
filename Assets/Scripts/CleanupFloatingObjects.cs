using UnityEngine;

public class CleanupFloatingObjects : MonoBehaviour
{
    // The list of floating objects to clean up (could be any GameObject with Rigidbody2D or similar)
    private GameObject[] floatingObjects;

    // Timer to track when the cleanup should happen
    private float cleanupTimer;
    public float cleanupInterval = 5f;

    void Start()
    {
        // Initialize the cleanup process
        floatingObjects = GameObject.FindGameObjectsWithTag("FloatingObject");
        cleanupTimer = 0f;
        Debug.Log("CleanupFloatingObjects initialized.");
    }

    void Update()
    {
        // Increment the timer and check if it’s time to clean up floating objects

        if (cleanupTimer >= cleanupInterval)
        {
            CleanupObjects();
            cleanupTimer = 0f; // Reset the timer
        }
    }

    void CleanupObjects()
    {
        // Loop through floating objects and log cleanup action
        foreach (GameObject obj in floatingObjects)
        {
            if (obj != null)
            {
                // Here, you can add actual cleanup code, such as destroying objects, or you can leave it as is
                // obj.SetActive(false); // Example of disabling object instead of destroying
                Debug.Log("Cleanup: Floating object found and prepared for cleanup - " + obj.name);
            }
        }
    }

    // This method could be called externally if needed to trigger cleanup manually
    public void TriggerCleanup()
    {
        Debug.Log("Manual cleanup triggered.");
        CleanupObjects();
    }
}
