using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrashAnalytics : MonoBehaviour
{
    public static CrashAnalytics Instance;

    // Separate crash counts for each level.
    private int level1CrashCount = 0;
    private int level2CrashCount = 0;

    // Replace with your actual Google Form submission URL to submit analytics to google form
    private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSf4Skfs1F32Xf94OdahiWTxecVDnRL7gh1WYF8pyjjkpHVr1g/formResponse";
    
    // Replace these with your actual Google Form field entry IDs.
    private const string level1CrashEntryField = "entry.1612333294"; // Column for Level 1 crashes
    private const string level2CrashEntryField = "entry.433608613"; 

    void Awake()
    {
        // Singleton pattern – persist across scenes.
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

    // Call this method whenever a crash occurs.
    public void RecordCrash()
    {
        // Look for the LevelIdentifier component in the current scene.
        LevelIdentifier levelIdentifier = FindObjectOfType<LevelIdentifier>();
        if (levelIdentifier != null)
        {
            if (levelIdentifier.levelID == "Level1")
            {
                level1CrashCount++;
                Debug.Log("Crash recorded for Level 1. Count: " + level1CrashCount);
            }
            else if (levelIdentifier.levelID == "Level2")
            {
                level2CrashCount++;
                Debug.Log("Crash recorded for Level 2. Count: " + level2CrashCount);
            }
            else
            {
                Debug.Log("Crash recorded in an untracked level: " + levelIdentifier.levelID);
            }
        }
        else
        {
            Debug.LogWarning("LevelIdentifier not found in the current scene!");
        }
    }

    // When the application quits, send the crash data for each level.
    void OnApplicationQuit()
    {
        StartCoroutine(SendCrashData());
    }

    IEnumerator SendCrashData()
    {
        WWWForm form = new WWWForm();
        form.AddField(level1CrashEntryField, level1CrashCount);
        form.AddField(level2CrashEntryField, level2CrashCount);

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Crash data sent successfully: Level 1 = " + level1CrashCount + ", Level 2 = " + level2CrashCount);
        }
        else
        {
            Debug.Log("Error sending crash data: " + www.error);
        }
    }
}
