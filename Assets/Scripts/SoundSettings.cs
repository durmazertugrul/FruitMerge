using UnityEngine;
using System;

public static class SoundSettings
{
    public static event Action OnChanged;

    public static bool MusicOn
    {
        get => PlayerPrefs.GetInt(Consts.SaveValues.Music_On, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Consts.SaveValues.Music_On, value ? 1 : 0);
            PlayerPrefs.Save(); // Save the changes to PlayerPrefs for webgl builds if player closes the tab or refreshes the page
            OnChanged?.Invoke();
        }
    }

    public static bool SfxOn
    {
        get => PlayerPrefs.GetInt(Consts.SaveValues.Sfx_On, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(Consts.SaveValues.Sfx_On, value ? 1 : 0);
            PlayerPrefs.Save(); // Save the changes to PlayerPrefs for webgl builds if player closes the tab or refreshes the page
            OnChanged?.Invoke();
        }
    }
}
