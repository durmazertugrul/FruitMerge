using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource.mute = !SoundSettings.MusicOn;
        SoundSettings.OnChanged += ApplySettings;
    }

    private void ApplySettings()
    {
        audioSource.mute = !SoundSettings.MusicOn;
    }

    private void OnDestroy()
    {
        SoundSettings.OnChanged -= ApplySettings;
    }

}
