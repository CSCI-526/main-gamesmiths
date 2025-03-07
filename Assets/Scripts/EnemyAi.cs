using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // Assign your enemy bullet prefab in the Inspector
    public GameObject enemyBulletPrefab;
    // Random shoot interval range
    public float minShootInterval = 2f;
    public float maxShootInterval = 5f;
    // How long the enemy remains visible when shooting
    public float visibleDuration = 0.5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Make enemy invisible initially
        SetVisibility(false);
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            // Wait for a random time before shooting
            float waitTime = Random.Range(minShootInterval, maxShootInterval);
            yield return new WaitForSeconds(waitTime);

            // Make enemy visible before shooting
            SetVisibility(true);

            // Shoot enemy bullet towards the player's current position
            Shoot();

            // Remain visible for a short duration, then hide again
            yield return new WaitForSeconds(visibleDuration);
            SetVisibility(false);
        }
    }

    // Adjusts the sprite renderer's alpha to show or hide the enemy
    void SetVisibility(bool visible)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = visible ? 1f : 0f;
            spriteRenderer.color = color;
        }
    }

    // Instantiates the enemy bullet and sets its direction towards the player
    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            // Find the player by tag; ensure your player GameObject is tagged "Player"
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Calculate direction from enemy to player
                Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
                // Instantiate the enemy bullet at the enemy's position
                GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
                // Pass the calculated direction to the bullet's behavior script
                EnemyBulletBehaviorScript bulletScript = bullet.GetComponent<EnemyBulletBehaviorScript>();
                if (bulletScript != null)
                {
                    bulletScript.direction = directionToPlayer;
                }
                Debug.Log("Enemy fired a bullet towards player at direction: " + directionToPlayer);
            }
            else
            {
                Debug.LogWarning("Player not found. Make sure your player is tagged as 'Player'.");
            }
        }
    }
}
