using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TMPro;  // 使用 TextMeshPro
using UnityEngine.UI; // 使用 UI 组件

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // all classes can access
    public int keyCount = 0;  // number of keys obtained
    public TextMeshProUGUI keyText; // ui to show the number of keys


    private int round;
    public GameObject canvas1; 
    public GameObject canvas2;


    public GameObject winPanel; // 绑定通关 UI
    private bool gamePaused = false; // 标记游戏是否暂停


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
        UpdateKeyUI();

        winPanel.SetActive(false); // 默认隐藏通关 UI

        round = PlayerPrefs.GetInt("round", 1);
        if (round == 1)
        {
            canvas1.SetActive(true);
            canvas2.SetActive(false);
        }
        else
        {
            canvas2.SetActive(true);
            canvas1.SetActive(false);
        }
    }

    public void AddKey()
    {
        keyCount++; // increase key num
        UpdateKeyUI();
    }

    private void UpdateKeyUI()
    {
        keyText.text = "Keys obtained: " + keyCount; // update ui
    }

    public void PlayerWins()
    {
        winPanel.SetActive(true); // 显示通关 UI
        Time.timeScale = 0; // 暂停游戏
        //winPanel.GetComponent<Button>().interactable = true; // 允许交互
        gamePaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // 恢复游戏
        
        gamePaused = false;
        PlayerPrefs.SetInt("round", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载场景
    }

}
