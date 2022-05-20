using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FullStoryForDescription
{
    [SerializeField] private StoryClassForDescription[] stories;
    [SerializeField] private GameMaster.LANG defaultLang = GameMaster.LANG.eng;

    private Dictionary<GameMaster.LANG, StoryClassForDescription> storiesByLang = new Dictionary<GameMaster.LANG, StoryClassForDescription>();

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

        foreach (StoryClassForDescription story in stories)
        {
            if (!storiesByLang.ContainsKey(story.Lang))
            {
                storiesByLang.Add(story.Lang, story);
            }
        }
    }

    public string GetDescriptionByLang(GameMaster.LANG langThatNeeded)
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
            return storiesByLang[langThatNeeded].Description;
        }
        else if (storiesByLang.ContainsKey(defaultLang))
        {
            return storiesByLang[defaultLang].Description;
        }
        else
        {
            return null;
        }
    }

    public string GetDefaultDescription()
    {
        return GetDescriptionByLang(defaultLang);
    }
}
