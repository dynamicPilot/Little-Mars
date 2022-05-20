using UnityEngine;
using UnityEngine.UI;

public class StoryTellingUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject background;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button finishButton;

    [Header("UI Scripts")]
    [SerializeField] private StoryTellingPieceUI pieceUI;
    [SerializeField] private UILanguage uILanguage;

    [Header("Options")]
    [SerializeField] private bool needToStopGameTime;
    [SerializeField] private bool needToHideOnAwake;

    [Header("Scripts")]
    [SerializeField] private TimeSpeedUI timeSpeedUI;

    private Story story;
    private int currentPieceIndex;

    private void Awake()
    {
        if (needToHideOnAwake)
        {
            panel.SetActive(false);
            background.SetActive(false);
        }
    }

    public void SetStory(Story newStory, GameMaster.LANG language, bool startTelling = true)
    {
        story = newStory;
        uILanguage.SetTexts(language);
        
        if (startTelling)
        {
            StartTellingStory();
        }
    }

    void StartTellingStory()
    {
        if (story == null)
        {
            return;
        }
        else if (story.Pieces == null)
        {
            return;
        }
        else if (story.Pieces.Length == 0)
        {
            return;
        }

        currentPieceIndex = 0;

        panel.SetActive(true);
        background.SetActive(true);

        if (needToStopGameTime)
            timeSpeedUI.PauseGameTime();

        ShowCurrentPiece();
    }

    void ShowCurrentPiece()
    {
        if (currentPieceIndex < story.Pieces.Length)
        {
            pieceUI.SetStoryPiece(story.Pieces[currentPieceIndex], currentPieceIndex);
            prevButton.gameObject.SetActive(currentPieceIndex != 0);
            nextButton.gameObject.SetActive(currentPieceIndex != (story.Pieces.Length - 1));
            finishButton.gameObject.SetActive(currentPieceIndex == (story.Pieces.Length - 1));
        }
        else
        {
            EndTellingStory();
        }
    }

    public void MoveToNextPiece(int direction)
    {
        currentPieceIndex += direction;

        if (currentPieceIndex < 0)
        {
            currentPieceIndex = 0;
        }

        ShowCurrentPiece();
    }


    public void EndTellingStory()
    {
        panel.SetActive(false);
        background.SetActive(false);
        pieceUI.StopAllCoroutines();
        timeSpeedUI.UnpauseGameTime();
    }
}
