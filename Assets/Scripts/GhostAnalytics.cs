using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GhostAnalytics : MonoBehaviour
{
    public static GhostAnalytics Instance;

    // Counter for ghost collisions.
    private int ghostCollisionCount = 0;

    // Google Form submission URL – Replace with your actual URL.
    private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc_x4Wx7SHmFg_g2_JhGXKgrZ6WlZs62Wltzwydlm9fri9HwA/formResponse";

    // Google Form field IDs (replace with your actual entry IDs)
    private const string levelField = "entry.1431482076";         // Level Identifier field
    private const string ghostCollisionField = "entry.1116857167";

    void Awake()
    {
        // Singleton pattern—persist across scenes.
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
    /// Call this method whenever the player collides with their ghost.
    /// </summary>
    public void RecordGhostCollision()
    {
        ghostCollisionCount++;
        Debug.Log("Ghost Collision Recorded. Total collisions: " + ghostCollisionCount);
    }


    public IEnumerator SendData(string level)
    {
        WWWForm form = new WWWForm();
        form.AddField(levelField, level);
        form.AddField(ghostCollisionField, ghostCollisionCount);

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (www.result != UnityWebRequest.Result.Success)
#else
        if (www.isNetworkError || www.isHttpError)
#endif
        {
            Debug.LogError("Error sending ghost analytics: " + www.error);
        }
        else
        {
            Debug.Log("Ghost analytics sent successfully for level " + level + 
                      ": Ghost Collisions = " + ghostCollisionCount);
        }
    }

    /// <summary>
    /// Optionally reset the analytics counters once the data is sent.
    /// </summary>
    public void ResetAnalytics()
    {
        ghostCollisionCount = 0;
    }
}
