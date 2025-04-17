using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;  // Bullet speed
    public float lifetime = 5f;  // How long the bullet exists before destroying
    public GameObject shooter;  // The entity that fired the bullet (Player or Clone)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);  // Destroy bullet after lifetime
    }

    void Update()
    {
        // Move the bullet forward
        rb.velocity = transform.forward * speed;
    }

    // Collision detection
    void OnCollisionEnter(Collision collision)
    {
        // If bullet hits anything other than the shooter, apply damage or effect
        if (collision.gameObject != shooter)
        {
            // Add your logic for damage here, or interaction with hit object
            Destroy(gameObject);  // Destroy the bullet on impact
        }
    }
}
