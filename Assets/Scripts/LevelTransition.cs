// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LevelTransition : MonoBehaviour
// {
//     [Tooltip("Build Index of your Tutorial Level 2 scene (as shown in Build Settings)")]
//     public int tutorialLevelBuildIndex = 2;

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (!other.CompareTag("Player"))
//             return;

//         // Clean up ghost data
//         PlayerController.ResetGhostData();

//         // Figure out which scene we're loading
//         int currentIndex = SceneManager.GetActiveScene().buildIndex;
//         int nextIndex    = currentIndex + 1;
//         Debug.Log($"[LevelTransition] currentIndex={currentIndex}, nextIndex={nextIndex}");

//         // Set the flag if—and only if—this is the tutorial level
//         bool isTutorial = nextIndex == tutorialLevelBuildIndex;
//         TutorialVideoTrigger.ShouldPlayTutorialVideo = isTutorial;
//         Debug.Log($"[LevelTransition] isTutorial={isTutorial}, flag→{TutorialVideoTrigger.ShouldPlayTutorialVideo}");

//         // Finally load it
//         SceneManager.LoadScene(nextIndex);
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Tooltip("Build Index of your Tutorial Level 2 scene (as shown in Build Settings)")]
    public int tutorialLevelBuildIndex = 2;

    [Tooltip("Name of your Main Menu scene")]
    public string mainMenuSceneName = "MainMenu";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // 1) Clean up ghost data
        PlayerController.ResetGhostData();

        // 2) Determine current and next scene indexes
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes  = SceneManager.sceneCountInBuildSettings;
        int lastIndex    = totalScenes - 1;

        Debug.Log($"[LevelTransition] currentIndex={currentIndex}, lastIndex={lastIndex}");

        // 3) If we're on the last level, go back to Main Menu
        if (currentIndex >= lastIndex)
        {
            Debug.Log("[LevelTransition] Last level reached—loading Main Menu");
            SceneManager.LoadScene(mainMenuSceneName);
            return;
        }

        // 4) Otherwise set up tutorial flag if needed
        int nextIndex = currentIndex + 1;
        bool isTutorial = nextIndex == tutorialLevelBuildIndex;
        TutorialVideoTrigger.ShouldPlayTutorialVideo = isTutorial;
        Debug.Log($"[LevelTransition] nextIndex={nextIndex}, isTutorial={isTutorial}");

        // 5) Load the next level
        SceneManager.LoadScene(nextIndex);
    }
}
