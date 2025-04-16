    using UnityEngine;
    using TMPro;
    using UnityEngine.SceneManagement;
    using TMPro;  // TextMeshPro
    using UnityEngine.UI; // 

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance; // all classes can access
        public int keyCount = 0;  // number of keys obtained
        public TextMeshProUGUI keyText; // ui to show the number of keys


        private int round;
        public GameObject canvas1; 
        public GameObject canvas2;


        public GameObject winPanel; // winning pannel gameobject
        private bool gamePaused = false; // true false trigger value

        public void MainMenuButtonClicked()
        {
            // Optionally resume game time.
            Debug.Log("kjooo--");
            Time.timeScale = 1;
            // Load the Main Menu scene.
            SceneManager.LoadScene("MainMenu");
            // Alternatively, if you're using build indices:
            // SceneManager.LoadScene(0);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // UpdateKeyUI();

            
            GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
            foreach (GameObject key in keys)
            {
                key.SetActive(false);
            }
            

            winPanel.SetActive(false); // change the trigger to falsify

            round = PlayerPrefs.GetInt("round", 1);
            canvas1.SetActive(true);
            canvas2.SetActive(true);}
        //     if (round == 1)
        //     {
        //         canvas1.SetActive(true);
                
        //         canvas2.SetActive(false);
        //     }
        //     else
        //     {
        //         canvas2.SetActive(true);
        //         canvas1.SetActive(false);
        //     }
        // }

        // public void AddKey()
        // {
        //     keyCount++; // increase key num
        //     UpdateKeyUI();
        // }

        // private void UpdateKeyUI()
        // {
        //     keyText.text = "Keys obtained: " + keyCount; // update ui
        // }

        public void PlayerWins()
        {
            // PlayerController.ResetGhostData();
            winPanel.SetActive(true); // UI
            Time.timeScale = 0; 
            //winPanel.GetComponent<Button>().interactable = true; 
            gamePaused = true;
        }

        public void RestartGame()
        {
            
            // PlayerController.ResetStaticData();

            Time.timeScale = 1; 
            gamePaused = false;
            PlayerPrefs.SetInt("round", 1);
            // OnPlayerDeath?.Invoke();
            // Destroy(gameObject);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 
        }

    }
