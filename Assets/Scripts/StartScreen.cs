using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    // Reference to the panel or the whole start screen UI GameObject.
    public GameObject startScreenPanel;
    // Optional: reference to the TextMeshProUGUI element if you want to change text dynamically.
    public TextMeshProUGUI instructionText;
    private bool gameStarted = false;

    private static bool hasAlreadyStarted = false; // Static flag to track if the game has started previously

    // Start is called before the first frame 
    // void OnEnable() {
    // Debug.Log("Key object is re-enabled: " + gameObject.name);
// }

    void Start()
    {
        if(hasAlreadyStarted){
            if(startScreenPanel != null){
                startScreenPanel.SetActive(false);
            }
            Time.timeScale = 1f;
        }
        else{
            // Pause the game at the start
            Time.timeScale = 0f;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Enter, Space or left mouse click
        if (!gameStarted && !hasAlreadyStarted &&(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            hasAlreadyStarted = true;
            // Hide the start screen UI
            if (startScreenPanel != null){
                startScreenPanel.SetActive(false);
            }
            // Resume the game
            Time.timeScale = 1f;
            
        } 
    }
}
