using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : MonoBehaviour
{
    public GameObject normalBulletPrefab;  // Bullet used by player (blue)
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            ShootBullet(normalBulletPrefab, "NormalBullet");
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet(GameObject bulletPrefab, string bulletType)
    {
        if (firePoint == null)
        {
            Debug.LogError("FireEvent: FirePoint is not assigned! Assign it in the Inspector.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (bulletController != null)
        {
            bulletController.bulletType = bulletType; // Optional: for later logic
        }
    }
}
