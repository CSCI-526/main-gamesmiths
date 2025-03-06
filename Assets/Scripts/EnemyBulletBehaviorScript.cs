using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBulletBehaviorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the bullet if it goes off-screen
        if (Mathf.Abs(transform.position.x) > 20f || Mathf.Abs(transform.position.y) > 20f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Bullet hits player
        {
            Debug.Log("Player Hit!");
            Destroy(gameObject); // Destroy the bullet
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
}
