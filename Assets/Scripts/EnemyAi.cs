using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    
    public GameObject bulletPrefab;  // Bullet prefab to instantiate
    public Transform firePoint;      // Position from where bullet will be fired
    public float fireRate = 2f;      // Time between shots
    public float visibilityDuration = 1f;  // Time enemy stays visible
    private SpriteRenderer spriteRenderer;
    private Transform player;        // Reference to the player transform


    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        firePoint = transform.Find("FirePoint"); // Auto-assign FirePoint
        StartCoroutine(EnemyBehavior());

        // spriteRenderer = GetComponent<SpriteRenderer>();
        // player = GameObject.FindGameObjectWithTag("Player").transform;  // Find player by tag
        // StartCoroutine(EnemyBehavior());
    }
    IEnumerator EnemyBehavior()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 4f));  // Random wait before appearing
            
            // Make enemy visible
            spriteRenderer.enabled = true;

            // Shoot bullet
            Shoot();

            // Stay visible for a short time
            yield return new WaitForSeconds(visibilityDuration);

            // Make enemy invisible
            spriteRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        // Calculate direction from enemy to player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Instantiate the bullet and set its velocity
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set bullet velocity towards the player’s current position (direction to player)
        rb.velocity = direction * 5f;  // Speed of the bullet can be adjusted
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
