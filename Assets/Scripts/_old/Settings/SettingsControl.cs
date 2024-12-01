using System.IO;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SettingsControl : SingletonPersistent<SettingsControl>
{
    [SerializeField] private float defaultMusicVolume = 0.7f;
    public float DefaultMusicVolume { get { return defaultMusicVolume; } }
    [SerializeField] private float defaultSoundsVolume = 0.7f;
    public float DefaultSoundsVolume { get { return defaultSoundsVolume; } }

    [SerializeField] private float defaultUIScale = 1f;
    public float DefaultUIScale { get { return defaultUIScale; } }

    [SerializeField] private GameMaster.LANG defaulLang = GameMaster.LANG.eng;

    public delegate void NewLanguage(int index);
    public event NewLanguage OnNewLanguage;

    private PlayerConfig playerConfig;
    private AudioControl audioControl;
    private RadioControl radioControl;

    private bool radioIsSet = false;
    private int tempLang = (int) GameMaster.LANG.eng;

    private void Start()
    {
        ReadConfigFile();

        // set sounds
        audioControl = AudioControl.Instance;
        radioControl = RadioControl.Instance;
        //radioIsSet = false;

        //SetSoundsSettings();
    }

    void ReadConfigFile()
    {
        string path = Application.persistentDataPath + "/" + "config.txt";
        if (!File.Exists(path))
        {
            // Create a file and create default config - eng lang
            playerConfig = new PlayerConfig(true, defaultMusicVolume, true, defaultSoundsVolume, (int)defaulLang);
            using (StreamWriter sw = File.CreateText(path))
            {
                string json = JsonUtility.ToJson(playerConfig);
                sw.WriteLine(json);
            }
        }
        else
        {
            // Read file and create config according to it
            string json = File.ReadAllLines(path)[0];
            playerConfig = JsonUtility.FromJson<PlayerConfig>(json);
            CheckLanguageByIndex();            
        }

        radioIsSet = false;

        if (!playerConfig.IsMusicOn)
        {
            playerConfig.MusicVolume = defaultMusicVolume;
        }

        if (!playerConfig.IsSoundsOn)
        {
            playerConfig.SoundsVolume = defaultSoundsVolume;
        }

        tempLang = playerConfig.Lang;

        if (OnNewLanguage != null)
        {
            OnNewLanguage.Invoke(playerConfig.Lang);
        }

        //Debug.Log("SettingsControl: read config ");
    }

    public void SetNewSoundSettingsForLevel(bool newIsMusicOn, bool newIsSoundsOn)
    {
        bool needWriteNew = false;
        if (playerConfig.IsMusicOn != newIsMusicOn)
        {
            playerConfig.IsMusicOn = newIsMusicOn;
            needWriteNew = true;
        }
        
        if (playerConfig.IsSoundsOn != newIsSoundsOn)
        {
            playerConfig.IsSoundsOn = newIsSoundsOn;
            needWriteNew = true;
        }
        
        radioIsSet = false;

        SetSoundsSettings();

        if (needWriteNew) WriteNewConfigFile();
    }

    public void SetNewSettings(bool newIsMusicOn, float newMusicVolume, bool newIsSoundsOn, float newSoundsVolume, int newLangIndex, float newScaleForUI)
    {
        if (newLangIndex != playerConfig.Lang)
        {
            //Debug.Log("SettingsControl: try to set new lang " + newLangIndex);
            playerConfig.Lang = newLangIndex;
            tempLang = playerConfig.Lang;
            CheckLanguageByIndex();

            if (OnNewLanguage != null)
            {
                OnNewLanguage.Invoke(playerConfig.Lang);
            }
        }

        playerConfig.IsMusicOn = newIsMusicOn;
        playerConfig.MusicVolume = newMusicVolume;

        playerConfig.IsSoundsOn = newIsSoundsOn;
        playerConfig.SoundsVolume = newSoundsVolume;

        playerConfig.ScaleForUI = newScaleForUI;

        radioIsSet = false;

        SetSoundsSettings();
        WriteNewConfigFile();
    }

    public void ResetToDefault()
    {
        SetNewSettings(true, defaultMusicVolume, true, defaultSoundsVolume, (int) defaulLang, defaultUIScale);
    }

    void WriteNewConfigFile()
    {
        string path = Application.persistentDataPath + "/" + "config.txt";
        using (StreamWriter sw = File.CreateText(path))
        {
            string json = JsonUtility.ToJson(playerConfig);
            sw.WriteLine(json);
        }
    }

    public void CheckLanguageByIndex()
    {
        if (playerConfig.Lang > (int)GameMaster.LANG.rus)
        {
            playerConfig.Lang = 0;
        }
    }

    public void TryNewLanguage(int langIndex)
    {
        if (langIndex != tempLang)
        {
            //Debug.Log("SettingsControl: try to set new lang " + langIndex);
            tempLang = langIndex;
            CheckLanguageByIndex();

            if (OnNewLanguage != null)
            {
                OnNewLanguage.Invoke(tempLang);
            }
        }
    }

    public void ResetLanguageSettings()
    {
        if (tempLang != playerConfig.Lang)
        {
            //Debug.Log("SettingsControl: reset lang " + tempLang);
            tempLang = playerConfig.Lang;
            CheckLanguageByIndex();

            if (OnNewLanguage != null)
            {
                OnNewLanguage.Invoke(playerConfig.Lang);
            }
        }
    }

    public void SetSoundsSettings()
    {
        //Debug.Log("SettingsControl: setting sounds ...");
        if (audioControl == null)
        {
            audioControl = AudioControl.Instance;
        }

        audioControl.SetNewSoundsVolumeLevel(playerConfig.SoundsVolume, defaultSoundsVolume);
        if (playerConfig.IsSoundsOn)
        {
            audioControl.TurnOnOffSounds(false);
        }
        else
        {
            audioControl.TurnOnOffSounds(true);
        }
            

        if (!radioIsSet)
        {
            // set sounds
            radioControl.SetRadioVolume(playerConfig.MusicVolume, defaultMusicVolume);
            radioControl.SetSourceVolume(playerConfig.MusicVolume, defaultMusicVolume);

            if (playerConfig.IsMusicOn)
            { 
                radioControl.StartPlayingRadio();
            }
            else
            {
                radioControl.StopPlayingRadio();
            }
            
            radioIsSet = true;
        }
    }

    public void InAirMusicVolume(float value)
    {
            radioControl.SetRadioVolume(value, DefaultMusicVolume);        
    }

    public void ResetRadioIsSet()
    {
        radioIsSet = false;
    }

    public void ReserRadioIsPaused()
    {
        radioControl.IsPaused = false;
    }

    public int GetLanguageIndex()
    {
        if (playerConfig != null)
        {
            return playerConfig.Lang;
        }

        return (int)defaulLang;
    }

    public GameMaster.LANG GetLanguage()
    {
        if (playerConfig != null)
        {
            return (GameMaster.LANG) playerConfig.Lang;
        }

        return defaulLang;
    }

    public float GetSoundsVolume()
    {
        if (playerConfig != null)
        {
            return playerConfig.SoundsVolume;
        }

        return defaultSoundsVolume;
    }

    public float GetMusicVolume()
    {
        if (playerConfig != null)
        {
            return playerConfig.MusicVolume;
        }

        return defaultMusicVolume;
    }

    public bool GetIsMusicOn()
    {
        if (playerConfig != null) return playerConfig.IsMusicOn;

        return true;
    }

    public float GetUIScale()
    {
        if (playerConfig != null)
        {
            return playerConfig.ScaleForUI;
        }

        return defaultUIScale;
    }

    public void PauseUnpauseRadio(bool value)
    {
        if (value)
        {
            //Debug.Log("SettingsControl: un pause radio");
            radioControl.ContinuePlayingRadio();
        }
        else
        {
            //Debug.Log("SettingsControl: pause radio");
            radioControl.PausePlayingRadio();
        }
    }

    public void PauseUnpauseSounds(bool value)
    {
        if (audioControl == null)
        {
            audioControl = AudioControl.Instance;
        }

        audioControl.TurnOnOffSounds(!value);
    }

    public bool GetIsSoundsOn()
    {
        if (playerConfig != null) return playerConfig.IsSoundsOn;

        return true;
    }
}
