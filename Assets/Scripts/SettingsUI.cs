using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Image musicTrack, sfxTrack;
    [SerializeField] private RectTransform musicKnob, sfxKnob;
    [SerializeField] private Button musicButton, sfxButton, mainMenuButton;
    [SerializeField] private Sprite onSprite, offSprite;
    [SerializeField] private float knobOnX, knobOffX;
    [SerializeField] private float slideDuration = 0.18f;

    private void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
        sfxButton.onClick.AddListener(ToggleSFX);
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.Scenes.Main_Menu);
        });

        Apply(musicTrack, musicKnob, SoundSettings.MusicOn, animate: false);
        Apply(sfxTrack, sfxKnob, SoundSettings.SfxOn, animate: false);
    }

    private void ToggleMusic()
    {
        SoundSettings.MusicOn = !SoundSettings.MusicOn;
        Apply(musicTrack, musicKnob, SoundSettings.MusicOn, animate: true);
    }

    private void ToggleSFX()
    {
        SoundSettings.SfxOn = !SoundSettings.SfxOn;
        Apply(sfxTrack, sfxKnob, SoundSettings.SfxOn, animate: true);
    }

    private void Apply(Image track, RectTransform knob, bool isOn, bool animate)
    {
        track.sprite = isOn ? onSprite : offSprite;
        float targetX = isOn ? knobOnX : knobOffX;
        knob.DOKill();

        if (animate == false) 
        {
            knob.anchoredPosition = new Vector2(targetX, knob.anchoredPosition.y);
        }
        else
        {
             knob.DOAnchorPosX(targetX, slideDuration).SetEase(Ease.OutQuad);
        }
    }

    private void OnDestroy()
    {
        musicKnob.DOKill();
        sfxKnob.DOKill();
    }
}