using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject ReloadGameButton;
    [SerializeField]
    private TMP_Text endGameMessage;


    public static UIManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowEndGameMessage(int finalScore)
    {
        ReloadGameButton.SetActive(true);
        endGameMessage.text = "Game Over!";
    }


    public void OnStartNewGame_UIEventHandler()
    {
        GameManager.Instance.StartGame(false);
        startPanel.SetActive(false);
    }

    public void OnReloadGameButton_UIEventHandler()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnLoadScoreStart_UIEventHandler()
    {
        GameManager.Instance.StartGame(true);
        startPanel.SetActive(false);
    }


   

}