using UnityEngine;
using UnityEngine.UI;

public class UILanguage : MonoBehaviour
{
    [Header("UI Text elements")]
    [SerializeField] private Text[] texts;

    [Header("Labels")]
    [SerializeField] private UIFullLabel[] fullLabels;

    private GameMaster.LANG lang = GameMaster.LANG.eng;
    public GameMaster.LANG Lang { get { return lang; } }

    private bool isNotFirstSet = false;
    private bool isBlocked = false;
    private void Awake()
    {
        isNotFirstSet = false;
        isBlocked = false;

        if (texts.Length != fullLabels.Length)
        {
            Debug.Log("UILanguage: arrays of text and labels are not corresponding right!");
            isBlocked = true;
        }
    }
    public void SetTexts(GameMaster.LANG newLang)
    {
        if (isBlocked)
        {
            return;
        }
        else if (lang == newLang && isNotFirstSet)
        {
            return;
        }

        lang = newLang;
        isNotFirstSet = true;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = fullLabels[i].GetLabelByLang(lang).Label;
        }
    }
}
