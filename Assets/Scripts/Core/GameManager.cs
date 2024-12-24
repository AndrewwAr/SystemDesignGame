using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private int cardMatchingScore = 10;

    public int score { get; private set; }

    private BoardManager boardManager;
    private AudioManager audioManager;
    private string playerPrefScoreKey = "score";

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();

        //StartGame();
    }

    private void SavingScoreToPlayerPref(int _score)
    {
        PlayerPrefs.SetInt(playerPrefScoreKey, _score);
        PlayerPrefs.Save();
    }

    private int GettingScoreFromPlayerPref()
    {
        return PlayerPrefs.GetInt(playerPrefScoreKey, 0);
    }

    public void StartGame(bool _loadScore)
    {
        if(_loadScore)
            score = GettingScoreFromPlayerPref();
        else 
            score = 0;

        UIManager.Instance.UpdateScoreUI(score);


        boardManager.GenerateBoard();
    }

    public void EndGame()
    {
        UIManager.Instance.ShowEndGameMessage(score);
        audioManager.PlayAudioSoundEffect(GameAudioClips.gameOver);

    }

    public void IncreamentScore()
    {
        score += cardMatchingScore;
        SavingScoreToPlayerPref(score);

        UIManager.Instance.UpdateScoreUI(score);
    }
}