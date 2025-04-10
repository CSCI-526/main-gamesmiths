using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LevelAnalyticsManager : MonoBehaviour
{
    public static LevelAnalyticsManager Instance;
    
    // Counters for level attempts and completions.
    private int totalAttempts = 0;
    private int levelCompletions = 0;
    
    // Google Form submission URL – update this with your actual URL.
    private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScfxI6db33wCFjY-1omY9tSAqWlLRcd1g00AX92RqpJbJrWFQ/formResponse";
    
    // Google Form field IDs; replace these with your actual entry IDs.
    private const string levelField = "entry.517172138";         // For Level Identifier
    private const string totalAttemptsField = "entry.1601655251";   // For Total Attempts
    private const string completionsField = "entry.294203406";     // For Successful Completions

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

    /// <summary>
    /// Call this when a level begins (i.e., an attempt is made).
    /// </summary>
    public void RecordLevelAttempt()
    {
        totalAttempts++;
        Debug.Log("Level attempt recorded. Total Attempts: " + totalAttempts);
    }

    /// <summary>
    /// Call this when the level is successfully completed.
    /// </summary>
    public void RecordLevelCompletion()
    {
        levelCompletions++;
        Debug.Log("Level completed! Total Completions: " + levelCompletions);
    }

    /// <summary>
    /// Calculates and returns the completion rate percentage.
    /// </summary>
    public float GetLevelCompletionRate()
    {
        if (totalAttempts == 0)
            return 0;
        return (float)levelCompletions / totalAttempts * 100f;
    }

    /// <summary>
    /// Sends the level analytics data (level identifier, total attempts, completions) to Google Forms.
    /// </summary>
    /// <param name="currentLevel">The current level identifier</param>
    public IEnumerator SendData(string currentLevel)
    {
        WWWForm form = new WWWForm();
        form.AddField(levelField, currentLevel);
        form.AddField(totalAttemptsField, totalAttempts);
        form.AddField(completionsField, levelCompletions);

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (www.result != UnityWebRequest.Result.Success)
#else
        if (www.isNetworkError || www.isHttpError)
#endif
        {
            Debug.LogError("Error sending level analytics data: " + www.error);
        }
        else
        {
            Debug.Log("Level analytics sent successfully for level " + currentLevel +
                      ": Attempts = " + totalAttempts + ", Completions = " + levelCompletions +
                      " (Completion Rate = " + GetLevelCompletionRate() + "%)");
        }
    }

    /// <summary>
    /// Optionally reset the level analytics counters.
    /// </summary>
    public void ResetAnalytics()
    {
        totalAttempts = 0;
        levelCompletions = 0;
    }
}
