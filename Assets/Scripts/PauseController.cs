// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class PauseController : MonoBehaviour
// {
//     [Header("Pause UI Elements")]
//     public GameObject returnToMenuButtonGO;  // assign your Return-to-Menu button
//     public GameObject resumeButtonGO;        // assign your Resume button

//     private bool isPaused = false;

//     void Start()
//     {
//         // hide both at startup
//         SetPauseUI(false);
//     }

//     // hooked up to your Pause button
//     public void TogglePause()
//     {
//         if (isPaused) Resume();
//         else         ShowPauseUI();
//     }

//     // show the pause buttons & stop time
//     void ShowPauseUI()
//     {
//         isPaused = true;
//         Time.timeScale = 0f;
//         SetPauseUI(true);
//     }

//     // resume gameplay & hide pause buttons
//     public void Resume()
//     {
//         isPaused = false;
//         Time.timeScale = 1f;
//         SetPauseUI(false);
//     }

//     // go back to main menu
//     public void GoToMainMenu()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("MainMenu");
//     }

//     // helper to show/hide only these two buttons
//     void SetPauseUI(bool show)
//     {
//         if (returnToMenuButtonGO != null) returnToMenuButtonGO.SetActive(show);
//         if (resumeButtonGO       != null) resumeButtonGO.SetActive(show);
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("Pause UI Elements")]
    public GameObject returnToMenuButtonGO;  // drag your Return-to-Menu button here
    public GameObject resumeButtonGO;        // drag your Resume button here

    private bool isPaused = false;

    void Start()
    {
        SetPauseUI(false);
    }

    /// <summary>
    /// Called by your on-screen Pause button (if you have one).
    /// </summary>
    public void TogglePause()
    {
        if (isPaused) Resume();
        else         ShowPauseUI();
    }

    /// <summary>
    /// Shows the pause UI and stops time.
    /// </summary>
    void ShowPauseUI()
    {
        isPaused = true;
        Time.timeScale = 0f;
        SetPauseUI(true);
        Debug.Log("Game Paused");
    }

    /// <summary>
    /// Hides the pause UI and resumes time.
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SetPauseUI(false);
        Debug.Log("Game Resumed");
    }

    /// <summary>
    /// Loads the Main Menu scene.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Helper to show/hide both buttons at once.
    /// </summary>
    void SetPauseUI(bool show)
    {
        if (returnToMenuButtonGO  != null) returnToMenuButtonGO .SetActive(show);
        if (resumeButtonGO        != null) resumeButtonGO       .SetActive(show);
    }

    void Update()
    {
        // P or Escape toggles pause/unpause
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            return;
        }

        if (!isPaused) return;

        // While paused, Space = Resume
        if (Input.GetKeyDown(KeyCode.P))
        {
            Resume();
            return;
        }

        // While paused, M = Main Menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            GoToMainMenu();
        }
    }
}
