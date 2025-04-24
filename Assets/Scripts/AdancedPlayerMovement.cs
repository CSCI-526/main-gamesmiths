using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 5f;
    public float boostedSpeed = 10f;
    public bool canDoubleJump = false;

    private CharacterController controller;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = defaultSpeed;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 moveVector = transform.right * move * currentSpeed;
        controller.Move(moveVector * Time.deltaTime);

        // Example jump logic
        if (Input.GetButtonDown("Jump"))
        {
            // Primary jump
            // ...
            if (canDoubleJump)
            {
                // Logic for second jump
                // ...
            }
        }
    }

    public IEnumerator ActivateSpeedBoost(float duration)
    {
        currentSpeed = boostedSpeed;
        yield return new WaitForSeconds(duration);
        currentSpeed = defaultSpeed;
    }

    public IEnumerator EnableDoubleJump(float duration)
    {
        canDoubleJump = true;
        yield return new WaitForSeconds(duration);
        canDoubleJump = false;
    }
}
