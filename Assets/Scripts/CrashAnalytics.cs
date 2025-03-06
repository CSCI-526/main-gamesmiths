using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CrashAnalytics : MonoBehaviour
{
    public static CrashAnalytics Instance;
    // Accumulate crashes across scene reloads during one session.
    public static int totalCrashCount = 0;
    
    // Replace with your actual Google Form submission URL and entry ID.
    private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSf4Skfs1F32Xf94OdahiWTxecVDnRL7gh1WYF8pyjjkpHVr1g/formResponse";
    private const string crashEntryField = "entry.1612333294";

    void Awake()
    {
        // Persist this object between scene reloads.
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
        totalCrashCount++;
        Debug.Log("Crash recorded. Total crashes this session: " + totalCrashCount);
    }

    // When the application quits, send the aggregated crash count.
    void OnApplicationQuit()
    {
        // Note: OnApplicationQuit may not be called in the Unity Editor, but will in a build.
        StartCoroutine(SendCrashData(totalCrashCount));
    }

    IEnumerator SendCrashData(int crashValue)
    {
        WWWForm form = new WWWForm();
        form.AddField(crashEntryField, crashValue);

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Aggregated crash data sent successfully: " + crashValue);
        }
        else
        {
            Debug.Log("Error sending aggregated crash data: " + www.error);
        }
    }
}











// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;

// public class CrashAnalytics : MonoBehaviour
// {
//     // Singleton instance to persist across scene reloads.
//     public static CrashAnalytics Instance;
//     // Static variable to track total crashes during this session.
//     public static int totalCrashCount = 0;
    
//     // Replace with your actual Google Form URL and entry field ID.
//     private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSf4Skfs1F32Xf94OdahiWTxecVDnRL7gh1WYF8pyjjkpHVr1g/formResponse";
//     private const string crashEntryField = "Number of Crashes.";

//     void Awake()
//     {
//         // Implement singleton pattern.
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     // Call this method each time the player crashes.
//     public void RecordCrash()
//     {
//         totalCrashCount++;
//         Debug.Log("Total crash count for this session: " + totalCrashCount);
//         // Optionally, send the updated crash count to your Google Form.
//         StartCoroutine(SendCrashData(totalCrashCount));
//     }

//     IEnumerator SendCrashData(int crashValue)
//     {
//         WWWForm form = new WWWForm();
//         form.AddField(crashEntryField, crashValue);
        
//         UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
//         yield return www.SendWebRequest();

//         if (www.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log("Crash data sent successfully: " + crashValue);
//         }
//         else
//         {
//             Debug.Log("Error sending crash data: " + www.error);
//         }
//     }
// }








































// using UnityEngine;
// using UnityEngine.Networking;
// using System.Collections;

// public class CrashAnalytics : MonoBehaviour
// {
//     // Replace these constants with your actual Google Form info.
//     private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSf4Skfs1F32Xf94OdahiWTxecVDnRL7gh1WYF8pyjjkpHVr1g/formResponse";
//     private const string crashEntryField = "Number of Crashes.";

//     public int crashCount = 0;

//     public void RecordCrash()
//     {
//         crashCount++;
//         StartCoroutine(SendCrashData(crashCount));
//     }

//     IEnumerator SendCrashData(int crashValue)
//     {
//         WWWForm form = new WWWForm();
//         form.AddField(crashEntryField, crashValue);

//         UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
//         yield return www.SendWebRequest();

//         if (www.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log("Crash data sent successfully.");
//         }
//         else
//         {
//             Debug.Log("Error sending crash data: " + www.error);
//         }
//     }
// }
