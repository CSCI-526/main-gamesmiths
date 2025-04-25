using UnityEngine;

public class WholesomeMover : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 45f;

    [Header("Vertical Float")]
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 3f;

    [Header("Pulsing Scale")]
    public float scaleAmplitude = 0.2f;
    public float scaleFrequency = 1f;

    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + floatOffset, initialPosition.z);

        float scaleOffset = Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;
        transform.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the deadly object! 💀");
            // Replace this with your actual death logic:
            Destroy(other.gameObject); 
        }
    }
}
