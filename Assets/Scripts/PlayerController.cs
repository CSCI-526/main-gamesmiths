using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static void ResetGhostData()
    {
        recordedPositions.Clear();
        shootingStartTimes.Clear();
        shootingDurations.Clear();
        ghostExists = false;
        ghostAbilityCollected = false;
    }

    public static void ResetStaticData()
    {
        recordedPositions.Clear();
        shootingStartTimes.Clear();
        shootingDurations.Clear();
        ghostExists = false;
        ghostAbilityCollected = false;
    }

    public static event Action OnPlayerDeath;
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float verticalSpeed = 5f;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;
    private float nextShootTime = 0f;
    public static bool ghostAbilityCollected = false;


    [Header("PowerUps & Effects")]
    public GameObject destroyEffect;
    public GameObject ghostPrefab;
    public GameObject clonePrefab;

    [Header("Shooting Data Recording")]
    private static List<float> shootingStartTimes = new List<float>();
    private static List<float> shootingDurations = new List<float>();
    private bool isShooting = false;
    private float shootStartTime;

    [Header("Movement Data Recording")]
    private static List<Vector2> recordedPositions = new List<Vector2>();

    [Header("Ghost Replay Controls")]
    private bool isRecording = true;
    private bool isReplaying = false;
    private static bool ghostExists = false;

    // [Header("Key Collection")]
    // private bool hasKey = false;

    void Start()
    {
        if (recordedPositions.Count > 0 && ghostExists)
        {
            transform.position = new Vector3(-1.5f, 0.2f, 0);
            StartGhostReplay();
        }
        else
        {
            PlayerPrefs.SetInt("round", 1);
            StartCoroutine(RecordMovement());
        }
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical");
        transform.position += Vector3.up * verticalInput * verticalSpeed * Time.deltaTime;
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRecording)
            {
                shootStartTime = Time.time;
                shootingStartTimes.Add(shootStartTime);
            }
            isShooting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isRecording && isShooting)
            {
                shootingDurations.Add(Time.time - shootStartTime);
            }
            isShooting = false;
        }

        if (isShooting && Time.time >= nextShootTime)
        {
            Shoot(bulletPrefab, "NormalBullet");
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private void Shoot(GameObject prefab, string bulletType)
    {
        GameObject bullet = Instantiate(prefab, firePoint.position, firePoint.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (bulletController != null)
        {
            bulletController.bulletType = bulletType;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.CompareTag("FinalBlock"))
        // {
        //     // Ensure the GameManager instance is available
        //     if(GameManager.Instance != null)
        //     {
        //         GameManager.Instance.RestartGame();
        //     }
        //     else
        //     {
        //         Debug.LogWarning("GameManager instance is missing.");
        //     }
        // }
        if (collision.gameObject.CompareTag("Ability"))
        {
            ghostAbilityCollected = true;
            ghostExists = true;
            isReplaying = false;
            isRecording = false;
            PlayerPrefs.SetInt("round", 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.CompareTag("ClonePowerUp"))
        {
            ActivateClonePowerUp();
            Destroy(collision.gameObject);
        }
        else
        {
            recordedPositions.Clear();
            shootingStartTimes.Clear();
            shootingDurations.Clear();
            PlayerPrefs.SetInt("round", 1);
            ghostExists = false;
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ActivateClonePowerUp()
    {
        if (clonePrefab != null)
        {
            Vector3 clonePosition = transform.position + new Vector3(2f, 0, 0);
            GameObject clone = Instantiate(clonePrefab, clonePosition, Quaternion.identity);

            clone.AddComponent<CloneMovement>().Initialize(this);
            clone.AddComponent<CloneShooting>().Initialize(this);
        }
    }

        public void ModifySpeedTemporarily(float multiplier, float duration)
    {
        StartCoroutine(ApplySpeedModifier(multiplier, duration));
    }

    private IEnumerator ApplySpeedModifier(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
    }






    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Key"))
        // {
        //     hasKey = true;
        //     // GameManager.Instance.AddKey();
        //     Destroy(other.gameObject);
        // }
        if (other.CompareTag("Door") )
        {
            PlayerPrefs.SetInt("round", 1);
            GameManager.Instance.PlayerWins();
        }
    }

    IEnumerator RecordMovement()
    {
        recordedPositions.Clear();
        shootingStartTimes.Clear();
        shootingDurations.Clear();

        while (isRecording)
        {
            recordedPositions.Add(transform.position);
            yield return new WaitForFixedUpdate();
        }
    }
    

    public void StartGhostReplay()
    {
        if (!isReplaying)
        {
            StartCoroutine(ReplayGhost());
        }
    }

    IEnumerator ReplayGhost()
    {
        isReplaying = true;
        GameObject ghost = Instantiate(ghostPrefab, recordedPositions[0], Quaternion.identity);
        ghost.transform.Rotate(0, 0, -90);

        Transform ghostFirePoint = new GameObject("GhostFirePoint").transform;
        ghostFirePoint.parent = ghost.transform;
        ghostFirePoint.localPosition = Vector3.zero;

        float startTime = Time.time;
        int moveIndex = 0;
        int shootIndex = 0;

        while (moveIndex < recordedPositions.Count || shootIndex < shootingStartTimes.Count)
        {
            if (moveIndex < recordedPositions.Count)
            {
                ghost.transform.position = recordedPositions[moveIndex];
                moveIndex++;
            }

            if (shootIndex < shootingStartTimes.Count)
            {
                float shootingTime = shootingStartTimes[shootIndex] - shootingStartTimes[0];

                if (Time.time - startTime >= shootingTime)
                {
                    float shootEndTime = shootingTime + shootingDurations[shootIndex];
                    StartCoroutine(ContinuousGhostShooting(ghostFirePoint, shootingTime, shootEndTime));
                    shootIndex++;
                }
            }

            yield return new WaitForFixedUpdate();
        }

        isReplaying = false;
        ghostExists = false;
    }

    IEnumerator ContinuousGhostShooting(Transform firePoint, float start, float end)
    {
        float shootTime = Time.time;

        while (Time.time - shootTime <= end - start)
        {
            GameObject ghostBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            BulletController bulletController = ghostBullet.GetComponent<BulletController>();

            if (bulletController != null)
            {
                bulletController.bulletType = "GhostBullet";
            }

            yield return new WaitForSeconds(shootCooldown);
        }
    }
}