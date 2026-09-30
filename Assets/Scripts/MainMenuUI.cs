using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            LevelSelection.SelectedLevel = PlayerPrefs.GetInt(Consts.Levels.Unlocked_Level, 1);
            SceneManager.LoadScene(Consts.Scenes.Game);
        });

        levelsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Levels);
        });

        settingsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Settings);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Start()
    {
        Vector3 originalScale = playButton.transform.localScale; 

        playButton.transform // animate for play button
            .DOScale(originalScale * 1.05f, 0.8f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetLink(playButton.gameObject);
    }
}