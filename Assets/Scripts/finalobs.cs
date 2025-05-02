using UnityEngine;

public class FinalObs : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public float xAmplitude = 2f;      // Distance to move left/right from the player
    public float yAmplitude = 1f;      // Distance to move up/down from the player
    public float xSpeed = 1f;          // Speed of horizontal movement
    public float ySpeed = 1.5f;        // Speed of vertical movement

    private Vector3 initialOffset;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("FinalObs: Player reference not set.");
            enabled = false;
            return;
        }

        // Initial offset from the player’s position when the game starts
        initialOffset = transform.position - player.position;
    }

    void Update()
    {
        if (player == null) return;

        // Calculate relative movement using sine wave
        float x = Mathf.Sin(Time.time * xSpeed) * xAmplitude;
        float y = Mathf.Cos(Time.time * ySpeed) * yAmplitude;

        // Update obstacle position relative to player
        Vector3 newPos = player.position + initialOffset + new Vector3(x, y, 0f);
        transform.position = newPos;
    }
}
