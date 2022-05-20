using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayNightTransition : MonoBehaviour
{
    [SerializeField] private Image nightCover;

    [Header("Options")]
    [SerializeField] private float finishCoverAlpha;
    [SerializeField] private int transferTime = 1;
    [SerializeField] private int stepsNumber = 2;

    [Header("Scripts")]
    [SerializeField] private GameTime gameTime;

    [SerializeField] int transitionDirection = 1;
    [SerializeField] float betweenTransitionUpdate = 0f;
    [SerializeField] float alphaStep = 0;
    [SerializeField] bool transitionInProcess = false;

    private void Awake()
    {
        nightCover.gameObject.SetActive(false);
        transitionDirection = 1;
        betweenTransitionUpdate = transferTime * 60f / (stepsNumber * gameTime.GameMinutesPerRealSecond);
        alphaStep = finishCoverAlpha / stepsNumber;
        transitionInProcess = false;
    }

    public void CheckForMakeTransfer(int currentHour)
    {
        if (transitionInProcess)
        {
            return;
        }

        if (currentHour + 1 == gameTime.NightStartHour)
        {
            // make to night transition
            ToNightTransition();
        }
        else if (currentHour + 1 == gameTime.NightEndHour)
        {
            ToDayTransition();
        }
    }

    public void StopTransfer()
    {
        StopAllCoroutines();

    }

    void ToNightTransition()
    {
        //Debug.Log("DayNightTransition: start to night transition");
        transitionDirection = 1;
        nightCover.gameObject.SetActive(true);
        nightCover.color = new Color(nightCover.color.r, nightCover.color.b, nightCover.color.g, 0);
        StartCoroutine(MakeTransition());
    }

    void ToDayTransition()
    {
        //Debug.Log("DayNightTransition: start to day transition");
        transitionDirection = -1;
        StartCoroutine(MakeTransition());
    }

    IEnumerator MakeTransition()
    {
        transitionInProcess = true;

        // add to alpha
        float step = transitionDirection * alphaStep;
        
        if (nightCover.color.a + step < 0)
        {
            step = nightCover.color.a;
        }
        //Debug.Log("DayNightTransition: transition step " + step);

        nightCover.color = new Color(nightCover.color.r, nightCover.color.b, nightCover.color.g, nightCover.color.a + step);

        if (nightCover.color.a >= finishCoverAlpha && transitionDirection == 1)
        {
            // to night
            //Debug.Log("DayNightTransition: end to night transition");
            transitionInProcess = false;
            yield break;
        }
        else if (nightCover.color.a == 0 && transitionDirection == -1)
        {
            // to day
            //Debug.Log("DayNightTransition: end to day transition");
            transitionInProcess = false;
            nightCover.gameObject.SetActive(false);
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(betweenTransitionUpdate);
            StartCoroutine(MakeTransition());
        }
    }
}
