using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class KeyDisabler : MonoBehaviour
{
    void Awake()
    {
        // This ensures the GameObject this script is attached to persists between scenes.
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DisableKeysAfterDelay());
    }

    private IEnumerator DisableKeysAfterDelay()
    {
        yield return new WaitForEndOfFrame();
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject key in keys)
        {
            key.SetActive(false);
        }
        Debug.Log("Keys have been disabled in the scene.");
    }
}
