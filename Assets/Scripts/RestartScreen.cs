using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartScreenController : MonoBehaviour
{
    // Assign this in the Inspector: the panel containing your game over UI.
    public GameObject restartPanel;

    void Awake()
    {
        // Ensure the panel starts hidden.
        if (restartPanel != null)
        {
            restartPanel.SetActive(false);
        }
    }

    void OnEnable()
    {
        // Subscribe to the player death event.
        PlayerController.OnPlayerDeath += ShowRestartScreen;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks.
        PlayerController.OnPlayerDeath -= ShowRestartScreen;
    }

    void ShowRestartScreen()
    {
        if (restartPanel != null)
        {
            restartPanel.SetActive(true);
        }
        // Optionally pause the game:
        Time.timeScale = 0;
    }

    // This method should be linked to your Restart button’s OnClick event.
    public void RestartGameButtonClicked()
    {
        // Resume game time if paused.
        Time.timeScale = 1;
        // Call your already defined restart method in GameManager.
        GameManager.Instance.RestartGame();
    }
    public void MainMenuButtonClicked()
    {
        // Optionally resume game time.
        Time.timeScale = 1;
        // Load the Main Menu scene.
        SceneManager.LoadScene("MainMenu");
        // Alternatively, if you're using build indices:
        // SceneManager.LoadScene(0);
    }
}
