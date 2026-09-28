using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpperUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;

    [SerializeField] private Image levelImage;
    [SerializeField] private TMP_Text targetLevelText;

    [Header("Score Punch")] //for score animation
    [SerializeField] private float punchStrength = 0.3f;
    [SerializeField] private float punchDuration = 0.2f;
    [SerializeField] private int punchVibrato = 5;
    [SerializeField] private float punchElasticity = 0.5f;

    private void Start()
    {
        LoadBestScore();
        levelImage.sprite = GameManager.instance.CurrentLevel.targetFruit.spriteRef;
        targetLevelText.text = "LEVEL " + GameManager.instance.CurrentLevel.levelNumber.ToString();
        GameManager.instance.OnScoreChanged += GameManager_OnScoreChanged;
        GameManager.instance.OnBestScoreChanged += GameManager_OnBestScoreChanged;
    }

    private void GameManager_OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();

        if (score == 0) return;

        scoreText.transform.DOComplete();
        scoreText.transform.DOPunchScale(Vector3.one * punchStrength, punchDuration, punchVibrato, punchElasticity);
    }

    private void GameManager_OnBestScoreChanged()
    {
        LoadBestScore();
    }

    private void LoadBestScore()
    {
        bestScoreText.text = GameManager.instance.LoadHighScore().ToString();
    }

    private void OnDestroy()
    {
        scoreText.transform.DOKill();

        if (GameManager.instance == null) return;

        GameManager.instance.OnScoreChanged -= GameManager_OnScoreChanged;
        GameManager.instance.OnBestScoreChanged -= GameManager_OnBestScoreChanged;
    }
}