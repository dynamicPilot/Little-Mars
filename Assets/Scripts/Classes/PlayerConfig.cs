using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerConfig
{
    [SerializeField] private bool isMusicOn;
    public bool IsMusicOn { get { return isMusicOn; } set { isMusicOn = value; } }

    [SerializeField] private float musicVolume;
    public float MusicVolume { get { return musicVolume; } set { musicVolume = value; } }

    [SerializeField] private bool isSoundsOn;
    public bool IsSoundsOn { get { return isSoundsOn; } set { isSoundsOn = value; } }

    [SerializeField] private float soundsVolume;
    public float SoundsVolume { get { return soundsVolume; } set { soundsVolume = value; } }

    [SerializeField] private int lang;
    public int Lang { get{ return lang; } set { lang = value; } }

    [SerializeField] private float scaleForUI;
    public float ScaleForUI { get { return scaleForUI; } set { scaleForUI = value; } }

    public PlayerConfig(bool newIsMusicOn = true, float newMusicVolume = 0.7f, bool newIsSoundsOn = true, float newSoundsVolume = 0.7f, int newLang = 0, float newScaleForUI = 1f)
    {
        isMusicOn = newIsMusicOn;
        musicVolume = newMusicVolume;

        isSoundsOn = newIsSoundsOn;
        soundsVolume = newSoundsVolume;

        scaleForUI = newScaleForUI;

        if (newLang < (int)GameMaster.LANG.rus + 1)
        {
            lang = newLang;
        }
        else
        {
            lang = 0;
        }
        
    }

    //public void SetNew
}
