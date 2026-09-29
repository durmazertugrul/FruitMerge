using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTile : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private LevelDatabase levelDatabase;
    [SerializeField] private GameObject unlockedVisual;
    [SerializeField] private GameObject lockedVisual;
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private Button button;
    [SerializeField] private Transform visualRoot;


    private bool isUnlocked;
    private Vector3 startingPoint;

    private void Start()
    {
        startingPoint = visualRoot.localPosition;

        //is there a data of this level
        bool contentExits = false;

        for (int i = 0; i < levelDatabase.levels.Length; i++)
        {
            if (levelDatabase.levels[i].levelNumber == levelNumber) 
            {
                contentExits = true; 
                break;
            }
        }


        //locked or unlocked
        int unlocked = PlayerPrefs.GetInt(Consts.Levels.Unlocked_Level, 1);
        
        if (contentExits && levelNumber <= unlocked) 
        {
            isUnlocked = true;
        }

        unlockedVisual.SetActive(isUnlocked);
        lockedVisual.SetActive(isUnlocked == false);
        
        if (isUnlocked)
            numberText.text = levelNumber.ToString();
        else
            numberText.text = "";

        button.onClick.AddListener(OnClicked);


    }
    private void OnClicked() 
    {
        if (isUnlocked)
        {
            LevelSelection.SelectedLevel = levelNumber;
            SceneManager.LoadScene(Consts.Scenes.Game);
        }
        else 
        {
            Shake();
        }
    }

    private void Shake() //animate level unlocked level tiles
    {
        visualRoot.DOKill();
        visualRoot.localPosition = startingPoint;
        visualRoot.DOShakePosition(0.3f, 10f, 20); //time,power and titration
    }


}
