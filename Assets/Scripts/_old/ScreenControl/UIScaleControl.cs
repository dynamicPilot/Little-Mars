using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIScaleControl : MonoBehaviour
{
    [SerializeField] private bool needScaleControl = true;
    [SerializeField] private CanvasScaler canvasScaler;

    private float basicHeight = 720f;
    private float delta = 720f;
    private float scaleForBasic = 1f;

    private void Awake()
    {
        if (needScaleControl) SetCorrectScaleAccordingToDisplay();
    }
    public void SetCorrectScaleAccordingToDisplay()
    {
        // read display properties
        Resolution currentDisplay = Screen.currentResolution;

        float scale = Mathf.Floor(((currentDisplay.height - basicHeight) / delta + scaleForBasic)*10f) / 10f;

        // add user config
        scale *= SettingsControl.Instance.GetUIScale();

        //Debug.Log("UIScaleControl: set new scale " + scale);
        canvasScaler.scaleFactor = scale;
    }
}
