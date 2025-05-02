using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [Tooltip("Build Index of your Tutorial Level 2 scene (as shown in Build Settings)")]
    public int tutorialLevelBuildIndex = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Clean up ghost data
        PlayerController.ResetGhostData();

        // Figure out which scene we're loading
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex    = currentIndex + 1;
        Debug.Log($"[LevelTransition] currentIndex={currentIndex}, nextIndex={nextIndex}");

        // Set the flag if—and only if—this is the tutorial level
        bool isTutorial = nextIndex == tutorialLevelBuildIndex;
        TutorialVideoTrigger.ShouldPlayTutorialVideo = isTutorial;
        Debug.Log($"[LevelTransition] isTutorial={isTutorial}, flag→{TutorialVideoTrigger.ShouldPlayTutorialVideo}");

        // Finally load it
        SceneManager.LoadScene(nextIndex);
    }
}
