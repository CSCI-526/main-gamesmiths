using UnityEngine;

public class PowerupFloat : MonoBehaviour
{
    public float floatAmplitude = 0.25f; // How high to float
    public float floatFrequency = 1f;    // How fast to float

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Simple floating using sine wave
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
    }
}
