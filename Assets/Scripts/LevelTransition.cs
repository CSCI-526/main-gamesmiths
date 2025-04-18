
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.ResetGhostData(); // Clean up any residual ghost data to ensure a smooth transition to the next level

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            Debug.Log("Loading next scene index: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
