using UnityEngine;

public class CloneShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // Assign the bullet prefab in Unity
    public Transform firePoint;      // Position where the bullet spawns
    private PlayerController player;

    public void Initialize(PlayerController playerController)
    {
        player = playerController;
    }

    void Update()
    {
        //if (player != null && Input.GetKeyDown(KeyCode.Space))  // Fire when the player fires
        //{
        //    Shoot();
        //}
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Clone Fired a Bullet!");
        }
    }
}