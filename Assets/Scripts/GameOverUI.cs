using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup hudGroup;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private TMP_Text scoreTextGO;      // gameover score text
    [SerializeField] private TMP_Text bestScoreTextGO;  // gameover high score text

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        tryAgainButton.onClick.AddListener(() =>
        {
            hudGroup.DOFade(1f, fadeDuration);
            hudGroup.interactable = true;
            hudGroup.blocksRaycasts = true;

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            GameManager.instance.NewGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Main_Menu);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Start()
    {
        GameManager.instance.OnGameOver += GameManager_OnGameOver;
        GameManager.instance.OnScoreChanged += GameManager_OnScoreChanged;
    }

    private void OnDestroy()
    {
        if (GameManager.instance == null) return;

        GameManager.instance.OnGameOver -= GameManager_OnGameOver;
        GameManager.instance.OnScoreChanged -= GameManager_OnScoreChanged;
    }

    private void GameManager_OnScoreChanged(int score)
    {
        scoreTextGO.text = score.ToString();
    }

    private void GameManager_OnGameOver()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        LoadBestScore();

        float delaySeconds = 1f;
        yield return new WaitForSeconds(delaySeconds);

        hudGroup.DOFade(0f, fadeDuration);
        canvasGroup.DOFade(1f, fadeDuration);

        hudGroup.interactable = false;
        hudGroup.blocksRaycasts = false;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void LoadBestScore()
    {
        bestScoreTextGO.text = GameManager.instance.LoadHighScore().ToString();
    }
}