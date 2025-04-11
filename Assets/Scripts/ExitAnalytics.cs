using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitAnalytics : MonoBehaviour
{
    public static ExitAnalytics Instance;
    
    private int mainMenuExitCount = 0;
    private const string googleFormURL = "https://docs.google.com/forms/d/e/1FAIpQLSdPpEy6aQTHaDnssXwMbOStED5ulOsjsqSRF--pZYgvNG77MQ/formResponse";
    private const string exitCountField = "entry.527598161";
    private const string sessionIDField = "entry.116254671";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void RecordMainMenuExit()
    {
        mainMenuExitCount++;
        Debug.Log("Main Menu Exit recorded. Total count: " + mainMenuExitCount);
    }
    
    public IEnumerator SendExitData()
    {
        WWWForm form = new WWWForm();
        form.AddField(exitCountField, mainMenuExitCount);

        string currentLevel = "Scene Name: " + SceneManager.GetActiveScene().name +
                              ", Build Index: " + SceneManager.GetActiveScene().buildIndex;
        form.AddField(sessionIDField, currentLevel);

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending exit analytics: " + www.error);
        }
        else
        {
            Debug.Log("Exit analytics sent successfully for " + currentLevel);
        }
    }
    
    public void ResetExitData()
    {
        mainMenuExitCount = 0;
    }
}
