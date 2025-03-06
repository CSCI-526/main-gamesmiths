using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : MonoBehaviour
{
    public GameObject bulletPrefabA;  // Bullet for first round
    public GameObject bulletPrefabB;  // Bullet for second round

    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    private int round = 1;  // Track the current round, default is round 1

    void Start()
    {
        // Add any necessary initialization if required
        round = PlayerPrefs.GetInt("round", 1);
    }

    void Update()
    {
        // Check if space key is pressed and it's time to shoot based on fire rate
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            
            Debug.Log("FireEvent -- Current Round: " + round);
            // Fire bullets based on the round
            if (round == 1)
            {
                ShootBullet(bulletPrefabA);  // First round bullet
            }
            else if (round == 2)
            {
                ShootBullet(bulletPrefabB);  // Second round bullet
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet(GameObject bulletPrefab)
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
