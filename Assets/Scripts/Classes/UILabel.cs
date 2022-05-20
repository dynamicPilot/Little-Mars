using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UILabel
{
    [SerializeField] private GameMaster.LANG lang;
    public GameMaster.LANG Lang { get { return lang; } }

    [SerializeField, TextArea (0, 1)] private string label;
    public string Label { get { return label; } }
}


[System.Serializable]
public class UIFullLabel
{
    [SerializeField] private UILabel[] labels;
    [SerializeField] private GameMaster.LANG defaultLang = GameMaster.LANG.eng;

    private Dictionary<GameMaster.LANG, UILabel> labelsByLang = new Dictionary<GameMaster.LANG, UILabel>();

    void FillLangs()
    {
        if (labels == null)
        {
            return;
        }
        else if (labels.Length == 0)
        {
            return;
        }

        foreach (UILabel label in labels)
        {
            if (!labelsByLang.ContainsKey(label.Lang))
            {
                labelsByLang.Add(label.Lang, label);
            }
        }
    }

    public UILabel GetLabelByLang(GameMaster.LANG langThatNeeded)
    {
        if (labelsByLang == null)
        {
            FillLangs();
        }
        else if (labelsByLang.Count == 0)
        {
            FillLangs();
        }


        if (labelsByLang.ContainsKey(langThatNeeded))
        {
            return labelsByLang[langThatNeeded];
        }
        else if (labelsByLang.ContainsKey(defaultLang))
        {
            return labelsByLang[defaultLang];
        }
        else
        {
            return null;
        }
    }

    public UILabel GetDefaultLabel()
    {
        return GetLabelByLang(defaultLang);
    }
}