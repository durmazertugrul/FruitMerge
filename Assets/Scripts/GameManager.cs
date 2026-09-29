using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private Board board;
    [SerializeField] private LevelDatabase levelDatabase;
    
    public LevelData CurrentLevel { get; private set; }
    public event Action OnGameOver;
    public event Action OnLevelCompleted;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnTargetProgressChanged;
    public event Action OnBestScoreChanged;

    private int score;
    private int bestScore;
    public bool isLevelActive;
    private int currentUnlockedLevel;

    private int producedCount;


    private void Awake()
    {
        for (int i = 0; i < levelDatabase.levels.Length; i++)
        {
            if (levelDatabase.levels[i].levelNumber == LevelSelection.SelectedLevel)
            {
                CurrentLevel = levelDatabase.levels[i];
                break;
            }
        }

        if (CurrentLevel == null)
        {
            CurrentLevel = levelDatabase.levels[0];
        }



        if (instance == null)
        {
            instance = this;
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
        producedCount = 0;
        OnTargetProgressChanged?.Invoke(CurrentLevel.targetCount);
        SetScore(0);
        OnBestScoreChanged?.Invoke();
        isLevelActive = true;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();

        board.enabled = true;
    }


    public void GameOver() 
    {
        isLevelActive = false;

        board.enabled = false;
        OnGameOver?.Invoke();
    }

    private void SetScore(int score)
    {
        this.score = score;

        OnScoreChanged?.Invoke(score);

        SaveBestScore();
    }

    public void FruitCreated(TileStateSO fruit) 
    {
        SetScore(score + fruit.point);

        if(isLevelActive == false)
        {
            return;
        }

        if (fruit == CurrentLevel.targetFruit) 
        {
            producedCount++;
            int leftCount = CurrentLevel.targetCount - producedCount;
            OnTargetProgressChanged?.Invoke(leftCount);

            if (producedCount >= CurrentLevel.targetCount) 
            {
                LevelCompleted();
            }
        }
    }

    private void LevelCompleted() 
    {
        isLevelActive = false;
        board.enabled = false;

        currentUnlockedLevel = PlayerPrefs.GetInt(Consts.Levels.Unlocked_Level, 1);
        
        if (CurrentLevel.levelNumber == currentUnlockedLevel && CurrentLevel.levelNumber < levelDatabase.levels.Length) 
        {
            PlayerPrefs.SetInt(Consts.Levels.Unlocked_Level, currentUnlockedLevel + 1 );
        }

        OnLevelCompleted?.Invoke();
    }

    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt(GetHighScoreKey(), 0);
    }

    private void SaveBestScore() 
    {
        bestScore = LoadHighScore();

        if(score > bestScore) 
        {
            PlayerPrefs.SetInt(GetHighScoreKey(), score);
           
        }
    }

    private string GetHighScoreKey() // One high score per level: BestScore_1, BestScore_2 ...
    {
        return Consts.SaveValues.Best_Score + "_" + CurrentLevel.levelNumber;
    }
}
