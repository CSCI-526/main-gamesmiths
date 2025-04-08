using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // To load scenes

public class MainMenu : MonoBehaviour
{
    // Called when the player clicks "Play Tutorial"
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial Level 1"); // Replace with your tutorial scene's name
    }

    // Called when the player clicks "Start Game"
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial Level 3"); // Replace with your main game scene's name
    }

    // Called when the player clicks "Exit Game"
    public void ExitGame()
    {
        Application.Quit(); // Quit the game (works in the built version, not in editor)
        Debug.Log("Game is quitting...");
    }
}

