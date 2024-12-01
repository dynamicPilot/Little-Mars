using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private string soundName;
    public string SoundName { get { return soundName; } }
    [SerializeField] private AudioClip clip;
    public AudioClip Clip { get { return clip; } }
    [SerializeField] private bool isLoop = false;
    public bool IsLoop { get { return isLoop; } }

    [SerializeField] [Range(0f, 1f)] private float defaultVolume = 0.7f;
    public float DefaultVolume { get { return defaultVolume; } }

    [SerializeField] [Range(0f, 1f)] private float volume = 0.7f;

    private AudioSource source;
    public void SetAudioSource(AudioSource newSource)
    {
        source = newSource;
        source.volume = volume;
        source.clip = clip;
        source.loop = isLoop;
    }

    public void SetNewVolume(float newVolume, bool needChangeSourceVolume = false)
    {
        volume = Mathf.Floor(newVolume * 100f) / 100f;

        if (needChangeSourceVolume)
        {
            if (source != null) source.volume = volume;
        }
    }

    //public void Play()
    //{
    //    source.Play();
    //}

    //public void Stop()
    //{
    //    source.Stop();
    //}
}
