using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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

}