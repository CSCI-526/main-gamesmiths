using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreenPanel;
    public TextMeshProUGUI instructionText;

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }
    }

    void Update()
    {
        if (!gameStarted && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            if (startScreenPanel != null)
            {
                startScreenPanel.SetActive(false);
            }
            Time.timeScale = 1f;
        } 
    }
}
