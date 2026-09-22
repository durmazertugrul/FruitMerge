using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpperUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButton;


    private void Awake()
    {
        newGameButton.onClick.AddListener(() =>
        {
            GameManager.instance.NewGame();
        });

        quitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    private void Start()
    {
        LoadBestScore();

        GameManager.instance.OnScoreChanged += GameManager_OnScoreChanged;
        GameManager.instance.OnBestScoreChanged += GameManager_OnBestScoreChanged;
    }

    private void GameManager_OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }

    private void GameManager_OnBestScoreChanged()
    {
        LoadBestScore();
    }


    private void LoadBestScore() 
    {
        bestScoreText.text = GameManager.instance.LoadHighScore().ToString();
    }


}

    

