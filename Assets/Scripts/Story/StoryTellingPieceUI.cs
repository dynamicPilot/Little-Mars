using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTellingPieceUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text header;
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    [Header("Options")]
    [SerializeField] private int charLimitToTypeWithEffect = 50;
    [SerializeField] private float updatePeriod = 0.2f;

    private StoryPiece storyPiece = null;
    private Animator animator;

    private void Awake()
    {
        animator = image.GetComponent<Animator>();
    }

    public void SetStoryPiece(StoryPiece newStoryPiece, int animatorIndex = 0)
    {
        StopAllCoroutines();
        storyPiece = newStoryPiece;

        if (storyPiece == null)
        {
            return;
        }

        // animator
        if (animator == null)
        {
            animator = image.GetComponent<Animator>();
        }

        // set header
        header.text = storyPiece.Header;

        // set image or animator controller
        image.gameObject.SetActive(true);
        if (storyPiece.AnimatorController != null)
        {
            animator.enabled = true;
            
            if (animator.runtimeAnimatorController != storyPiece.AnimatorController)
            {
                animator.runtimeAnimatorController = storyPiece.AnimatorController;
            }

            try
            {
                animator.SetFloat("index", animatorIndex);
            }
            catch
            {

            }
        }
        else if (storyPiece.Icon != null)
        {
            animator.enabled = false;
            image.sprite = storyPiece.Icon;
        }
        else
        {
            image.gameObject.SetActive(false);
        }

        // set text
        StartCoroutine(ShowText(storyPiece.Description));
    }

    IEnumerator ShowText(string sentence)
    {
        text.text = "";
        int counter = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            counter++;

            if (counter <= charLimitToTypeWithEffect)
                yield return new WaitForSecondsRealtime(updatePeriod);
        }
    }
}
