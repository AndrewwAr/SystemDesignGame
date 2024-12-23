using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text scoreText;
    public GameObject endGamePanel;
    public TMP_Text endGameMessage;

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
        endGamePanel.SetActive(true);
        endGameMessage.text = "Game Over!\nFinal Score: " + finalScore;
    }
}