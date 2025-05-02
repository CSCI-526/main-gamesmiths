using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLevelTransition : MonoBehaviour
{
    [Header("Transition Settings")]
    public string nextSceneName;              // Name of the next scene to load
    public float fadeDuration = 1.5f;         // How long the fade takes
    public Image fadeImage;                   // UI Image used to fade in/out
    public bool autoStartFadeIn = true;       // Should fade-in happen on scene start?

    private bool isTransitioning = false;

    void Start()
    {
        if (autoStartFadeIn && fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            isTransitioning = true;
            StartCoroutine(TransitionToNextLevel());
        }
    }

    IEnumerator TransitionToNextLevel()
    {
        yield return StartCoroutine(FadeOut());

        // Optional: Save current level progress
        PlayerPrefs.SetString("LastLevel", nextSceneName);
        PlayerPrefs.Save();

        // Load next scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}

