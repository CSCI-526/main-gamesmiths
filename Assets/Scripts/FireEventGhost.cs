using UnityEngine;

public class FireEventGhost : MonoBehaviour
{
    public GameObject ghostBulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            ShootBullet(ghostBulletPrefab, "GhostBullet");
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet(GameObject bulletPrefab, string bulletType)
    {
        if (firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (bulletController != null)
        {
            bulletController.bulletType = bulletType;
        }
    }
}
