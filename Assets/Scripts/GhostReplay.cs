// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GhostReplay : MonoBehaviour
// {
//     private List<Vector2> recordedPositions;
//     private List<float> recordedShootingTimes;
//     private GameObject bulletPrefab;
//     private float replayStartTime;
//     private Transform firePoint;
//     private int currentShotIndex = 0;

//     public void Initialize(List<Vector2> positions, List<float> shootingTimes, GameObject bulletPrefab, float startTime)
//     {
//         this.recordedPositions = positions;
//         this.recordedShootingTimes = shootingTimes;
//         this.bulletPrefab = bulletPrefab;
//         this.replayStartTime = startTime;

//         firePoint = new GameObject("GhostFirePoint").transform;
//         firePoint.parent = this.transform;
//         firePoint.localPosition = Vector3.zero;

//         StartCoroutine(Replay());
//     }

//     IEnumerator Replay()
//     {
//         int index = 0;
//         float startTime = Time.time;

//         while (index < recordedPositions.Count)
//         {
//             transform.position = recordedPositions[index];

//             // Handle shooting based on recorded times
//             if (currentShotIndex < recordedShootingTimes.Count &&
//                 Time.time - startTime >= recordedShootingTimes[currentShotIndex])
//             {
//                 Shoot();
//                 currentShotIndex++;
//             }

//             index++;
//             yield return new WaitForFixedUpdate();
//         }
//     }

//     void Shoot()
//     {
//         GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//         BulletController bulletController = bullet.GetComponent<BulletController>();
//         if (bulletController != null)
//         {
//             bulletController.bulletType = "GhostBullet"; // Ensure ghost shoots GhostBullets
//         }

//         Debug.Log("Ghost shot a GhostBullet");
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostReplay : MonoBehaviour
{
    private List<Vector2> recordedPositions;
    private List<float> recordedShootingTimes;
    private GameObject bulletPrefab;
    private float replayStartTime;
    private Transform firePoint;
    private int currentShotIndex = 0;

    public void Initialize(List<Vector2> positions, List<float> shootingTimes, GameObject bulletPrefab, float startTime)
    {
        this.recordedPositions = positions;
        this.recordedShootingTimes = shootingTimes;
        this.bulletPrefab = bulletPrefab;
        this.replayStartTime = startTime;

        firePoint = new GameObject("GhostFirePoint").transform;
        firePoint.parent = this.transform;
        firePoint.localPosition = Vector3.zero;

        StartCoroutine(Replay());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // When a new scene is loaded, destroy this ghost
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Destroy(gameObject);
    }
     private void OnPlayerDeath()
    {
        Destroy(gameObject);
    }

    IEnumerator Replay()
    {
        int index = 0;
        float startTime = Time.time;

        while (index < recordedPositions.Count)
        {
            transform.position = recordedPositions[index];

            // Handle shooting based on recorded times
            if (currentShotIndex < recordedShootingTimes.Count &&
                Time.time - startTime >= recordedShootingTimes[currentShotIndex])
            {
                Shoot();
                currentShotIndex++;
            }

            index++;
            yield return new WaitForFixedUpdate();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.bulletType = "GhostBullet"; // Ensure ghost shoots GhostBullets
        }

        Debug.Log("Ghost shot a GhostBullet");
    }
}
