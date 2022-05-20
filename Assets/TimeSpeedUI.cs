using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpeedUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text sign;
    [SerializeField] private Image imageToChangeColor;

    [Header("Settings")]
    [SerializeField] private float highSpeed = 2f;
    [SerializeField] private float colorChangeInterval = 1f;
    [SerializeField] private Color colorToChange;
    [SerializeField] private Color defaultColor;

    // 0 - normal, 1 - fast
    private int counter = 0;
    private bool coroutineIsRunning = false;

    private void Awake()
    {
        counter = 0;
        sign.text = ">>";
        coroutineIsRunning = false;
    }
    public void ChangeTimeSpeed()
    {
        counter++;
        counter %= 2;
        SetTimeScaleByCounter();
    }

    public void PauseGameTime()
    {
        Time.timeScale = 0f;
        ChangeTimeSpeedButtonSign();
    }

    public void UnpauseGameTime()
    {
        SetTimeScaleByCounter();
    }

    void SetTimeScaleByCounter()
    {
        Time.timeScale = 1 + highSpeed * counter;
        ChangeTimeSpeedButtonSign();
    }

    void ChangeTimeSpeedButtonSign()
    {
        if (Time.timeScale == 0f)
        {
            StopColorChange();
            sign.text = "||";
        }
        else if (Time.timeScale == 1f)
        {
            StopColorChange();
            sign.text = ">>";
        }
        else
        {
            if (!coroutineIsRunning) StartCoroutine(ColorChangeDueToHighSpeed());
            sign.text = ">";
        }
    }

    void StopColorChange()
    {
        StopAllCoroutines();
        coroutineIsRunning = false;
        imageToChangeColor.color = defaultColor;
    }

    IEnumerator ColorChangeDueToHighSpeed()
    {
        coroutineIsRunning = true;
        yield return new WaitForSecondsRealtime(colorChangeInterval);

        imageToChangeColor.color = colorToChange;
        
        yield return new WaitForSecondsRealtime(colorChangeInterval);

        imageToChangeColor.color = defaultColor;
        coroutineIsRunning = false;

        StartCoroutine(ColorChangeDueToHighSpeed());
    }
}
