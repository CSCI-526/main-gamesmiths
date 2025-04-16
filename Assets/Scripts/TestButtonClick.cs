using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TestButtonClick : MonoBehaviour
{
    public void TestClick()
    {
        Debug.Log("Test button click received.");
        // Load the Main Menu scene. This is the Button that redirects to MainMenu scene.
        // this is not the test button this is the real working button, The MainMenuButtonClicked inside
        // RestartSceen.cs file is not at all working. 
        SceneManager.LoadScene("MainMenu");
    }
}
