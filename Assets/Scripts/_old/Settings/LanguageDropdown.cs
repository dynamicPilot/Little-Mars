using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    private GameMaster.LANG currentLang = GameMaster.LANG.eng;
    private List<GameMaster.LANG> langs = new List<GameMaster.LANG>();

    public void SetDropdownOptions(GameMaster.LANG newCurrentLang)
    {
        //Debug.Log("LangiageDropdown: set options, current lang " + newCurrentLang);
        langs.Clear();
        currentLang = newCurrentLang;
        for (int i = 0; i <= (int)GameMaster.LANG.rus; i++)
        {
            langs.Add((GameMaster.LANG) i);
        }

        SetDropdownText();
        dropdown.value = (int) currentLang;
    }

    void SetDropdownText()
    {
        foreach(GameMaster.LANG lang in langs)
        {
            if (lang == GameMaster.LANG.eng)
            {
                dropdown.options.Add(new Dropdown.OptionData("English"));
            }
            else if (lang == GameMaster.LANG.rus)
            {
                dropdown.options.Add(new Dropdown.OptionData("Русский"));
            }
        }
    }

    public void SetCurrentOption(int optionIndex)
    {
        currentLang = langs[optionIndex];
    }

    public GameMaster.LANG GetLanguage()
    {
        return currentLang;
    }
}
