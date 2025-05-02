using UnityEngine;
using UnityEngine.UI;

public class SpeedSensitivePlayer : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float fastSpeed = 10f;
    public float slowSpeed = 2f;
    public Text statusText;

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
        UpdateStatus("Normal Speed");
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveInput = new Vector3(moveX, 0, moveZ);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * currentSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FastZone"))
        {
            currentSpeed = fastSpeed;
            UpdateStatus("Speed Boost!");
        }
        else if (other.CompareTag("SlowZone"))
        {
            currentSpeed = slowSpeed;
            UpdateStatus("Slow Down!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FastZone") || other.CompareTag("SlowZone"))
        {
            currentSpeed = normalSpeed;
            UpdateStatus("Normal Speed");
        }
    }

    void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
            CancelInvoke(nameof(ClearStatus));
            Invoke(nameof(ClearStatus), 2f);
        }
    }

    void ClearStatus()
    {
        statusText.text = "";
    }
}
