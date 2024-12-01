using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FullStory
{
    [SerializeField] private Story[] stories;
    [SerializeField] private GameMaster.LANG defaultLang = GameMaster.LANG.eng;

    private Dictionary<GameMaster.LANG, Story> storiesByLang = new Dictionary<GameMaster.LANG, Story>();

    void FillLangs()
    {
        if (stories == null)
        {
            return;
        }
        else if (stories.Length == 0)
        {
            return;
        }

        foreach (Story story in stories)
        {
            if (!storiesByLang.ContainsKey(story.Lang))
            {
                storiesByLang.Add(story.Lang, story);
            }
        }
    }

    public Story GetStoryByLang(GameMaster.LANG langThatNeeded)
    {
        if (storiesByLang == null)
        {
            FillLangs();
        }
        else if (storiesByLang.Count == 0)
        {
            FillLangs();
        }


        if (storiesByLang.ContainsKey(langThatNeeded))
        {
            return storiesByLang[langThatNeeded];
        }
        else if (storiesByLang.ContainsKey(defaultLang))
        {
            return storiesByLang[defaultLang];
        }
        else
        {
            return null;
        }
    }

    public Story GetDefaultLangStory()
    {
        return GetStoryByLang(defaultLang);
    }
}

