using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : MonoBehaviour
{
    public GameObject normalBulletPrefab;  // Bullet used in the first round, appears (blue)
    public GameObject ghostBulletPrefab;   // Bullet used in the second round, appears (pink)

    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    private int round = 1;  // Track the current round, default is round 1

    void Start()
    {
        round = PlayerPrefs.GetInt("round", 1); // Get the round from PlayerPrefs
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Debug.Log("FireEvent -- Current Round: " + round);

            if (round == 1)
            {
                ShootBullet(normalBulletPrefab, "NormalBullet"); // Fire normal bullet
            }
            else if (round == 2)
            {
                ShootBullet(ghostBulletPrefab, "GhostBullet"); // Fire ghost bullet
            }
            nextFireTime = Time.time + fireRate;
        }
    }

   void ShootBullet(GameObject bulletPrefab, string bulletType)
{
    if (firePoint == null)
    {
        Debug.LogError("FireEvent: FirePoint is not assigned! Assign it in the Inspector.");
        return; // Stop execution to prevent error
    }

    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    BulletController bulletController = bullet.GetComponent<BulletController>();

    if (bulletController != null)
    {
        bulletController.bulletType = bulletType; // Assign bullet type
    }
}
}