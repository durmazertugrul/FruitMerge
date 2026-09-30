using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;



public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private CanvasGroup hudGroup;
    [SerializeField] private CanvasGroup boardGroup;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private float fadeDuration = 0.5f;

    [SerializeField] private TMP_Text scoreTextGO;      // gameover score text
    [SerializeField] private TMP_Text bestScoreTextGO;  // gameover high score text
    [SerializeField] private TMP_Text newRecordLabel;  
    [SerializeField] private TMP_Text levelLabel;

    private CanvasGroup canvasGroup;
    

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        tryAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Game);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Main_Menu);
        });

        continueButton.onClick.AddListener(() =>
        {            
            LevelSelection.SelectedLevel++; 
            SceneManager.LoadScene(Consts.Scenes.Game);
        });
    }

    private void Start()
    {
        GameManager.instance.OnLevelCompleted += GameManager_OnLevelCompleted;
        GameManager.instance.OnScoreChanged += GameManager_OnScoreChanged;
    }

    private void OnDestroy()
    {
        newRecordLabel.DOKill();
        newRecordLabel.transform.DOKill();
        if (GameManager.instance == null) return;
        GameManager.instance.OnLevelCompleted -= GameManager_OnLevelCompleted;
        GameManager.instance.OnScoreChanged -= GameManager_OnScoreChanged;
    }

    private void GameManager_OnScoreChanged(int score)
    {
        scoreTextGO.text = score.ToString();
    }

    private void GameManager_OnLevelCompleted()
    {
        levelText.text = "LEVEL " + GameManager.instance.CurrentLevel.levelNumber.ToString();
        bool hasNext = GameManager.instance.HasLevel(GameManager.instance.CurrentLevel.levelNumber + 1);
        continueButton.gameObject.SetActive(hasNext);
        StartCoroutine(LevelCompleted());
    }

    private IEnumerator LevelCompleted()
    {
        LoadBestScore();
        newRecordLabel.gameObject.SetActive(false);

        float delaySeconds = 0.2f;
        yield return new WaitForSeconds(delaySeconds);

        hudGroup.DOFade(0f, fadeDuration);
        canvasGroup.DOFade(1f, fadeDuration);
        boardGroup.DOFade(0f, fadeDuration);

        hudGroup.interactable = false;
        hudGroup.blocksRaycasts = false;
        boardGroup.interactable = false;
        boardGroup.blocksRaycasts = false;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;


        yield return new WaitForSeconds(fadeDuration);

        if (GameManager.instance.IsNewRecord)
        {
            newRecordLabel.gameObject.SetActive(true);

            bestScoreTextGO.transform.DOKill();
            bestScoreTextGO.transform.DOPunchScale(Vector3.one * 0.5f, 0.4f, 7, 0.7f);

            newRecordLabel.DOKill();
            newRecordLabel.alpha = 1;
            newRecordLabel.DOFade(0.25f, 0.8f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        }
        else
        {
            newRecordLabel.gameObject.SetActive(false);
        }

    }

    private void LoadBestScore()
    {
        levelLabel.text = "LEVEL " + GameManager.instance.CurrentLevel.levelNumber + " BEST SCORE:";
        bestScoreTextGO.text = GameManager.instance.LoadHighScore().ToString();
    }
}
