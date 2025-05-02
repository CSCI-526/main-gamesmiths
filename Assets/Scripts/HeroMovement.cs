using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public int lives = 1;

    private Rigidbody rb;
    private bool canJump = true;
    private int currentLives;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLives = lives;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z) * moveSpeed;
        rb.MovePosition(transform.position + movement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            canJump = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
            canJump = true;

        if (col.gameObject.CompareTag("Threat"))
            TakeHit();
    }

    void TakeHit()
    {
        currentLives--;
        Debug.Log("Hero damaged! Remaining lives: " + currentLives);

        if (currentLives <= 0)
            ArenaManager.Instance.OnHeroDeath();
    }
}
