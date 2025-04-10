using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CrashAnalytics : MonoBehaviour
{
    public static CrashAnalytics Instance;

    // Generalized crash tracking
    private Dictionary<string, int> crashCounts = new Dictionary<string, int>();

    // Google Form details
    // private const string googleFormURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSf4Skfs1F32Xf94OdahiWTxecVDnRL7gh1WYF8pyjjkpHVr1g/formResponse";
    private const string googleFormURL = "https://docs.google.com/forms/d/e/1FAIpQLSdPpEy6aQTHaDnssXwMbOStED5ulOsjsqSRF--pZYgvNG77MQ/formResponse";


    private readonly Dictionary<string, string> formFieldIDs = new Dictionary<string, string>()
{
    { "Tutorial Level 1", "entry.914140386" },
    { "Tutorial Level 2", "entry.237606257" },
    { "Tutorial Level 3", "entry.701435032" },
    { "Level 04", "entry.1644832459" },
    { "Level 05", "entry.360948607" },
    // Ghost failure fields
{ "Tutorial Level 1_GhostFail", "entry.1979294385" },
{ "Tutorial Level 2_GhostFail", "entry.1226837414" },
{ "Tutorial Level 3_GhostFail", "entry.7251485711" },
{ "Level 01_GhostFail", "entry.285882207" },
{ "Level 02_GhostFail", "entry.1081790364" },

};


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

    public void RecordCrash()
    {
        LevelIdentifier levelIdentifier = FindObjectOfType<LevelIdentifier>();
        if (levelIdentifier != null)
        {
            string levelID = levelIdentifier.levelID;

            if (!crashCounts.ContainsKey(levelID))
            {
                crashCounts[levelID] = 0;
            }

            crashCounts[levelID]++;
            Debug.Log($"Crash recorded for {levelID}. Count: {crashCounts[levelID]}");
        }
        else
        {
            Debug.LogWarning("LevelIdentifier not found in current scene!");
        }
    }

    void OnApplicationQuit()
    {
        StartCoroutine(SendCrashData());
    }

    public IEnumerator SendCrashData()
{
    WWWForm form = new WWWForm();

    foreach (var entry in crashCounts)
    {
        string level = entry.Key;
        int count = entry.Value;

        if (formFieldIDs.ContainsKey(level))
        {
            form.AddField(formFieldIDs[level], count);
            Debug.Log($"[SEND] Crash Level: {level}, Count: {count}, FieldID: {formFieldIDs[level]}");
        }
        else
        {
            Debug.LogWarning($"[SKIPPED] Crash Level '{level}' has no matching Form Entry ID.");
        }
    }

    foreach (string level in ghostFailureDeaths)
    {
        string ghostFailKey = level + "_GhostFail";
        if (formFieldIDs.ContainsKey(ghostFailKey))
        {
            form.AddField(formFieldIDs[ghostFailKey], "Yes");
            Debug.Log($"[SEND] Ghost Fail: {level} → FieldID: {formFieldIDs[ghostFailKey]}");
        }
        else
        {
            Debug.LogWarning($"[SKIPPED] Ghost fail level '{level}' has no matching GhostFail Field ID.");
        }
    }

    UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
    yield return www.SendWebRequest();

    if (www.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("✅ Crash data sent successfully to Google Form!");
    }
    else
    {
        Debug.LogError("❌ Error sending crash data: " + www.error);
    }
}


public List<string> ghostFailureDeaths = new List<string>();

public void RecordGhostFailureDeath()
{
    string level = GetCurrentLevelID(); // helper we'll define
    if (!ghostFailureDeaths.Contains(level))
    {
        ghostFailureDeaths.Add(level);
        Debug.Log($"[Analytics] Player died without using ghost in {level}");
    }
}

private string GetCurrentLevelID()
{
    LevelIdentifier levelIdentifier = FindObjectOfType<LevelIdentifier>();
    return levelIdentifier != null ? levelIdentifier.levelID : "Unknown";
}




}
