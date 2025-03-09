using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevelName = "Tutorial Level 2"; // Set this to the name of the next level

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure Player has the "Player" tag
        {
            Debug.Log("Level Complete! Loading next level...");
            SceneManager.LoadScene(nextLevelName);
        }
    }
}