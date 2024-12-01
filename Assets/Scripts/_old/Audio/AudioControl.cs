using UnityEngine;

[DefaultExecutionOrder(-2)]
public class AudioControl : Singleton<AudioControl>
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource clickUISource;
    [SerializeField] private AudioSource notificationUISource;
    [SerializeField] private AudioSource gameStateSource;
    [SerializeField] private AudioSource actionUISource;
    [SerializeField] private AudioSource doubleActionUISource;

    [Header("Others")]
    [SerializeField] private int canNotDoItSoundIndex = 0;
    //[SerializeField] private int quitSoundIndex = 2;

    [Header("Scripts")]
    [SerializeField] private SoundStorage soundStorage;
    //[SerializeField] private SettingsControl settingsControl;

    private bool actionSoundIsBusy = false;


    public void TurnOnOffSounds(bool isMute)
    {
        if (clickUISource.mute == isMute)
        {
            return;
        }

        clickUISource.mute = isMute;
        notificationUISource.mute = isMute;
        gameStateSource.mute = isMute;
        actionUISource.mute = isMute;
        doubleActionUISource.mute = isMute;
    }

    public void PlayCanNotDo()
    {
        PlayGameStateSound(canNotDoItSoundIndex);
    }

    public void PlayClickSound(int clickIndex = 0)
    {
        if (clickIndex < 0 || clickIndex > soundStorage.ShortSounds.Length - 1)
        {
            return;
        }
        soundStorage.ShortSounds[clickIndex].SetAudioSource(clickUISource);
        clickUISource.Play();
    }

    public void PlayNotificationSound(int notificationIndex)
    {
        if (notificationIndex < 0 || notificationIndex > soundStorage.NotificationSounds.Length - 1)
        {
            return;
        }
        soundStorage.NotificationSounds[notificationIndex].SetAudioSource(notificationUISource);
        notificationUISource.Play();
    }

    public void PlayGameStateSound(int notificationIndex)
    {
        if (notificationIndex < 0 || notificationIndex > soundStorage.StateSounds.Length - 1)
        {
            return;
        }
        soundStorage.StateSounds[notificationIndex].SetAudioSource(gameStateSource);
        gameStateSource.Play();
    }

    public void PlayActionSound(int actionIndex)
    {
        if (actionIndex < 0 || actionIndex > soundStorage.ActionSounds.Length - 1)
        {
            return;
        }
        soundStorage.ActionSounds[actionIndex].SetAudioSource(doubleActionUISource);
        doubleActionUISource.Play();
    }


    public void StartActionSound(int actionIndex)
    {
        if (actionSoundIsBusy)
        {
            return;
        }
        else if (actionIndex < 0 || actionIndex > soundStorage.ActionSounds.Length - 1)
        {
            return;
        }
        
        soundStorage.ActionSounds[actionIndex].SetAudioSource(actionUISource);
        actionUISource.Play();
        actionSoundIsBusy = true;
    }

    public void StopActionSound(int actionIndex)
    {
        if (!actionSoundIsBusy) return;
        actionUISource.Stop();
        actionSoundIsBusy = false;
    }

    public void SetNewSoundsVolumeLevel(float newVolume, float defaultSoundsVolume)
    {
        //audioSources[i].volume = value * initialEffectsVolume[i] / _initialVolumeLevel;
        soundStorage.SetNewVolumeToSounds(newVolume, defaultSoundsVolume);
    }
}
