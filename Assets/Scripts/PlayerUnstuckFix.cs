using UnityEngine;

public class PlayerUnstuckFix : MonoBehaviour
{
    public Transform fallbackPosition; // A safe point to teleport to
    public float checkInterval = 1.5f;
    public float stuckThreshold = 0.1f;
    public float autoUnstuckTime = 3.0f;
    public KeyCode manualUnstuckKey = KeyCode.U;

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    private float checkTimer = 0f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        checkTimer += Time.deltaTime;

        if (checkTimer >= checkInterval)
        {
            float distanceMoved = Vector3.Distance(transform.position, lastPosition);

            if (distanceMoved < stuckThreshold)
            {
                stuckTimer += checkTimer;

                if (stuckTimer >= autoUnstuckTime)
                {
                    Debug.LogWarning("Player auto-unstuck triggered.");
                    Unstuck();
                    stuckTimer = 0f;
                }
            }
            else
            {
                stuckTimer = 0f;
            }

            lastPosition = transform.position;
            checkTimer = 0f;
        }

        if (Input.GetKeyDown(manualUnstuckKey))
        {
            Debug.Log("Manual unstuck key pressed.");
            Unstuck();
        }
    }

    void Unstuck()
    {
        if (fallbackPosition != null)
        {
            transform.position = fallbackPosition.position;
        }
        else
        {
            Debug.LogError("No fallbackPosition set on PlayerUnstuckFix script.");
        }
    }
}
