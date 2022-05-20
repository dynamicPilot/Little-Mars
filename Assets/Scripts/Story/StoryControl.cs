using UnityEngine;

public class StoryControl : MonoBehaviour
{
    [SerializeField] private FullStory fullStoryForLevelTraining;
    [SerializeField] private StoryTellingUI storyTellingUI;
    [SerializeField] private GameMaster gameMaster;

    public void SetStoryForLevel(FullStory newFullStory)
    {
        fullStoryForLevelTraining = newFullStory;
    }

    public bool HaveTrainingStory()
    {
        return fullStoryForLevelTraining != null;
    }

    public void StartTellingStory()
    {
        if (fullStoryForLevelTraining == null)
        {
            return;
        }
        else
        {
            storyTellingUI.SetStory(fullStoryForLevelTraining.GetStoryByLang(gameMaster.Lang), gameMaster.Lang);
        }
    }
}
