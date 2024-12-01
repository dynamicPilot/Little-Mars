using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStorage : MonoBehaviour
{
    [Header("UI Sounds")]
    [SerializeField] private Sound[] shortSounds;
    public Sound[] ShortSounds { get { return shortSounds; } }

    [Header("Notification UI Sounds")]
    [SerializeField] private Sound[] notificationSounds;
    public Sound[] NotificationSounds { get { return notificationSounds; } }

    [Header("Game State Notification Sounds")]
    [SerializeField] private Sound[] stateSounds;
    public Sound[] StateSounds { get { return stateSounds; } }


    [Header("Action Sounds")]
    [SerializeField] private Sound[] actionSounds;
    public Sound[] ActionSounds { get { return actionSounds; } }


    public void SetNewVolumeToSounds(float newVolume, float defaultSoundsVolume)
    {
        foreach(Sound sound in shortSounds)
        {
            sound.SetNewVolume(newVolume * sound.DefaultVolume / defaultSoundsVolume);
        }

        foreach (Sound sound in notificationSounds)
        {
            sound.SetNewVolume(newVolume * sound.DefaultVolume / defaultSoundsVolume);
        }

        foreach (Sound sound in stateSounds)
        {
            sound.SetNewVolume(newVolume * sound.DefaultVolume / defaultSoundsVolume);
        }

        foreach (Sound sound in actionSounds)
        {
            sound.SetNewVolume(newVolume * sound.DefaultVolume / defaultSoundsVolume);
        }
    }

}
