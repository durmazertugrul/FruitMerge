using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private float fadeDuration = 1f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        tryAgainButton.onClick.AddListener(() => 
        {
            GameManager.instance.NewGame();
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        });



        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        GameManager.instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver() 
    {
        int deleySeconds = 1;
        yield return new WaitForSeconds(deleySeconds);

        canvasGroup.DOFade(1f, fadeDuration);
        canvasGroup.interactable = true;

    }

}
