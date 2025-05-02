// using System.Collections;
// using UnityEngine;

// [RequireComponent(typeof(Animator), typeof(AudioSource))]
// public class PlayerActionManager : MonoBehaviour
// {
//     // Input keys
//     public KeyCode jumpKey = KeyCode.Space;
//     public KeyCode attackKey = KeyCode.Mouse0;
//     public KeyCode interactKey = KeyCode.E;
//     public KeyCode dodgeKey = KeyCode.LeftShift;
//     public KeyCode specialKey = KeyCode.Q;

//     // Audio clips
//     public AudioClip jumpClip;
//     public AudioClip attackClip;
//     public AudioClip interactClip;
//     public AudioClip dodgeClip;
//     public AudioClip specialClip;

//     // Particle effects
//     public GameObject attackEffect;
//     public GameObject dodgeEffect;
//     public GameObject specialEffect;

//     // Cooldowns
//     public float dodgeCooldown = 1.5f;
//     public float specialCooldown = 5f;

//     private bool canDodge = true;
//     private bool canSpecial = true;

//     private Animator animator;
//     private AudioSource audioSource;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         audioSource = GetComponent<AudioSource>();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(jumpKey))
//             HandleJump();

//         if (Input.GetKeyDown(attackKey))
//             HandleAttack();

//         if (Input.GetKeyDown(interactKey))
//             HandleInteract();

//         if (Input.GetKeyDown(dodgeKey) && canDodge)
//             StartCoroutine(HandleDodge());

//         if (Input.GetKeyDown(specialKey) && canSpecial)
//             StartCoroutine(HandleSpecial());
//     }

//     void HandleJump()
//     {
//         animator.SetTrigger("Jump");
//         PlaySound(jumpClip);
//         Debug.Log("Player jumped");
//     }

//     void HandleAttack()
//     {
//         animator.SetTrigger("Attack");
//         PlaySound(attackClip);
//         if (attackEffect != null)
//         {
//             Instantiate(attackEffect, transform.position + transform.forward, Quaternion.identity);
//         }
//         Debug.Log("Player attacked");
//     }

//     void HandleInteract()
//     {
//         animator.SetTrigger("Interact");
//         PlaySound(interactClip);
//         Debug.Log("Player interacted with an object");
        
//     IEnumerator HandleDodge()
//     {
//         canDodge = false;
//         animator.SetTrigger("Dodge");
//         PlaySound(dodgeClip);
//         if (dodgeEffect != null)
//         {
//             Instantiate(dodgeEffect, transform.position, Quaternion.identity);
//         }
//         Debug.Log("Player dodged");
//         yield return new WaitForSeconds(dodgeCooldown);
//         canDodge = true;
//     }

//     IEnumerator HandleSpecial()
//     {
//         canSpecial = false;
//         animator.SetTrigger("Special");
//         PlaySound(specialClip);
//         if (specialEffect != null)
//         {
//             Instantiate(specialEffect, transform.position, Quaternion.identity);
//         }
//         Debug.Log("Player used special ability!");
//         yield return new WaitForSeconds(specialCooldown);
//         canSpecial = true;
//     }

//     void PlaySound(AudioClip clip)
//     {
//         if (clip != null)
//             audioSource.PlayOneShot(clip);
//     }
// }
// }