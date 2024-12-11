using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; private set; }
    private bool isGameOver;

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

    public void StartGame()
    {
        score = 0;
        isGameOver = false;
        BoardManager boardManager = FindObjectOfType<BoardManager>();
        boardManager.GenerateBoard();
    }

    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.ShowEndGameMessage(score);
    }

    public void AddScore(int points)
    {
        score += points;
        UIManager.Instance.UpdateScoreUI(score);
    }
}