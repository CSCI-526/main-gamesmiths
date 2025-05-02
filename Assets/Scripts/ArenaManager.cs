using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnHeroDeath()
    {
        Debug.Log("Hero has fallen. Restarting arena...");
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
