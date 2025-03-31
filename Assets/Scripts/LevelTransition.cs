// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LevelTransition : MonoBehaviour
// {
//     public string nextLevelName = "Tutorial Level 2"; // Set this to the name of the next level

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player")) // Ensure Player has the "Player" tag
//         {
//             Debug.Log("Level Complete! Loading next level...");
//             SceneManager.LoadScene(nextLevelName);
//         }
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.ResetGhostData(); // Clear ghost data before transitioning to the next level

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("Loading next scene index: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
