using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class RadioControl : SingletonPersistent<RadioControl>
{
    [SerializeField] private Playlist playlist;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] int currentSongIndex;
    [SerializeField] bool isPaused = false;
    public bool IsPaused { get{ return isPaused; } set { isPaused = value; } }

    IEnumerator PlayingRadio()
    {
        if (currentSongIndex >= playlist.Songs.Length)
        {
            //Debug.Log("RadioControl: Reset Radio Index");
            currentSongIndex = 0;
        }
        else if (currentSongIndex < 0)
        {
            currentSongIndex = playlist.Songs.Length - 1;
        }

        for (int i = currentSongIndex; i < playlist.Songs.Length; i++)
        {
           // Debug.Log("RadioControl: Coroutine: start play index...." + currentSongIndex);
            playlist.Songs[i].SetAudioSource(audioSource);
            audioSource.Play();
            yield return new WaitForSecondsRealtime(audioSource.clip.length);
            //Debug.Log("RadioControl: Coroutine: end play index...." + currentSongIndex);
            currentSongIndex++;
        }

        StartCoroutine(PlayingRadio());
    }

    public void PausePlayingRadio()
    {
        //Debug.Log("RadioControl: pause radio....");
        audioSource.Pause();
        isPaused = true;
    }

    public void ContinuePlayingRadio()
    {
        if (isPaused)
        {
            //Debug.Log("RadioControl: up pause radio....");
            audioSource.UnPause();
            isPaused = false;
        }
        else if(!audioSource.isPlaying)
        {
            //Debug.Log("RadioControl: start playing radio....");
            StartPlayingRadio();
        }
    }

    public void StartPlayingRadio()
    {
        StopAllCoroutines();
        if (audioSource.isPlaying)
        {
            //Debug.Log("RadioControl: is playing radio....");
            return;
        }
        else if (isPaused)
        {
            //Debug.Log("RadioControl: up pause radio....");
            audioSource.UnPause();
            isPaused = false;
            return;
        }

        //Debug.Log("RadioControl: start radio....");
        currentSongIndex = 0;
        isPaused = false;
        StartCoroutine(PlayingRadio());
    }

    public void StopPlayingRadio()
    {
        StopAllCoroutines();
        audioSource.Stop();
        isPaused = false;
    }

    public void SetRadioVolume(float newVolume, float defaultSoundsVolume)
    {
        foreach (Sound sound in playlist.Songs)
        {
            sound.SetNewVolume(newVolume * sound.DefaultVolume / defaultSoundsVolume, audioSource.clip == sound.Clip);
        }
    }

    public void SetSourceVolume(float newVolume, float defaultSoundsVolume)
    {
        playlist.Songs[currentSongIndex].SetNewVolume(newVolume * playlist.Songs[currentSongIndex].DefaultVolume / defaultSoundsVolume, true);
    }
}
