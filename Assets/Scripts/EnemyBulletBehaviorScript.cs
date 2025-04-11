using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBulletBehaviorScript : MonoBehaviour
{

    // Speed at which the bullet moves
    public float speed = 10f;
    // How long the bullet exists before being destroyed
    public float lifetime = 3f;
    // Set the direction of travel. Adjust if necessary (e.g., Vector2.left means the bullet moves to the left).
    public Vector2 direction = Vector2.left;

    private Rigidbody2D rb;

    // void Update()
    // {
    //     // Destroy the bullet if it goes off-screen (you can adjust these values as needed)
    //     if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set bullet velocity
        rb.velocity = direction.normalized * speed;
        // Destroy bullet after lifetime seconds
        Destroy(gameObject, lifetime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with the player (make sure your player is tagged as "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy bullet!");
            PlayerController.TriggerPlayerDeath();

            // Optionally disable the player so they do not interact further.
            other.gameObject.SetActive(false);

            // Destroy the enemy bullet.
            Destroy(gameObject);
            // SceneManager.LoadScene("MainMenu");
            // GameManager.Instance.RestartGame();
            
        }
    }
}
