using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private Board board;

    public event Action OnGameOver;
    public event Action<int> OnScoreChanged;
    public event Action OnBestScoreChanged;

    private int score;
    private int bestScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
    }



    public void NewGame() 
    {
        SetScore(0);
        OnBestScoreChanged?.Invoke();

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();

        board.enabled = true;
    }


    public void GameOver() 
    {
        board.enabled = false;
        OnGameOver?.Invoke();

    }

    public void IncreaseScore(int points) 
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;

        OnScoreChanged?.Invoke(score);

        SaveBestScore();
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(Consts.SaveValues.BEST_SCORE, 0);
    }

    private void SaveBestScore() 
    {
        bestScore = LoadHighScore();

        if(score > bestScore) 
        {
            PlayerPrefs.SetInt(Consts.SaveValues.BEST_SCORE, score);
           
        }
    }
}
