// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float normalSpeed = 5f;
//     public float slowSpeed = 2f;
//     public Rigidbody rb;

//     private float currentSpeed;
//     private bool isInSlowZone = false;

//     private void Start()
//     {
//         if (rb == null)
//             rb = GetComponent<Rigidbody>();

//         currentSpeed = normalSpeed;
//     }

//     private void Update()
//     {
//         MovePlayer();
//     }

//     private void MovePlayer()
//     {
//         float moveHorizontal = Input.GetAxis("Horizontal");
//         float moveVertical = Input.GetAxis("Vertical");

//         Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * currentSpeed;
//         rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("SlowZone"))
//         {
//             isInSlowZone = true;
//             currentSpeed = slowSpeed;
//         }
//         else if (other.CompareTag("FastObstacle"))
//         {
//             // Fast obstacle hits player - optional effect
//             rb.AddForce(Vector3.back * 500f); // Example: knock player backwards
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("SlowZone"))
//         {
//             isInSlowZone = false;
//             currentSpeed = normalSpeed;
//         }
//     }
// }
