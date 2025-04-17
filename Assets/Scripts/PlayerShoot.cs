using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet prefab to shoot
    public Transform shootPoint;     // Where the bullet will be spawned from
    public float shootCooldown = 0.5f;  // Cooldown time between shots
    private float nextShootTime = 0f;  // Track when the player can shoot again

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShootTime)  // Assuming Fire1 is left mouse click
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        // Spawn the bullet at shoot point
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.shooter = gameObject;  // Assign the shooter (Player)
        nextShootTime = Time.time + shootCooldown;  // Reset the shoot cooldown
    }
}
