// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MainMenu : MonoBehaviour
// {
//     // Tutorial level buttons
//     public void LoadTutorialLevel1()
//     {
//         SceneManager.LoadScene("Tutorial Level 1");
//     }

//     public void LoadTutorialLevel2()
//     {
//         TutorialVideoTrigger.ShouldPlayTutorialVideo = true;
//         SceneManager.LoadScene("Tutorial Level 2");
//     }

//     public void LoadTutorialLevel3()
//     {
//         SceneManager.LoadScene("Tutorial Level 3");
//     }

//     // Main level buttons
//     public void LoadLevel04()
//     {
//         SceneManager.LoadScene("Level 04");
//     }

//     public void LoadLevel05()
//     {
//         SceneManager.LoadScene("Level 05");
//     }
//     public void LoadLevel06()
//     {
//         SceneManager.LoadScene("Level 06");
//     }

//     // Optional: Quit button
//     public void ExitGame()
//     {
//         Application.Quit();
//         Debug.Log("Game is quitting...");
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Tutorial level buttons
    public void LoadTutorialLevel1()
    {
        SceneManager.LoadScene("Tutorial Level 1");
    }
    
    public void LoadTutorialLevel2()
    {
       TutorialVideoTrigger.ShouldPlayTutorialVideo = true;
        SceneManager.LoadScene("Tutorial Level 2");
    }
    
    public void LoadTutorialLevel3()
    {
        SceneManager.LoadScene("Tutorial Level 3");
    }
    
    // Main level buttons
    public void LoadLevel04()
    {
        SceneManager.LoadScene("Level 04");
    }
    
    public void LoadLevel05()
    {
        SceneManager.LoadScene("Level 05");
    }
    
    public void LoadLevel06()
    {
        SceneManager.LoadScene("Level 06");
    }
    
    // Optional: Quit button
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}