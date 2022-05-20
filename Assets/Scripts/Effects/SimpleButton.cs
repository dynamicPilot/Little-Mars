using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Settings")]
    [SerializeField] private Vector3 pointerDownScale;
    [SerializeField] private Color pointerDownColor;
    [SerializeField] private Color pointerDownTextColor;
    [SerializeField] private int clickSoundIndex = 0;

    [Header("Options")]
    [SerializeField] private bool needScaleChange;
    [SerializeField] private bool needColorChange;
    [SerializeField] private bool needTextColorChange;
    [SerializeField] private bool needClickSound = false;

    [Header("UI Elements")]
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    private AudioControl audioControl;

    private Vector3 defaultScale;
    private Color defaultColor;
    private Color defaultTextColor;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultScale = rectTransform.localScale;

        if (image != null)
        {
            defaultColor = image.color;
        }
        else
        {
            needColorChange = false;
        }

        if (text != null)
        {
            defaultTextColor = text.color;
        }
        else
        {
            needTextColorChange = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (rectTransform.localScale != pointerDownScale && needScaleChange)
            rectTransform.localScale = pointerDownScale;

        if (needColorChange)
        {
            image.color = pointerDownColor;
        }

        if (needTextColorChange)
        {
            text.color = pointerDownTextColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (rectTransform.localScale != defaultScale && needScaleChange)
            rectTransform.localScale = defaultScale;

        if (needColorChange)
        {
            image.color = defaultColor;
        }

        if (needTextColorChange)
        {
            text.color = defaultTextColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (needClickSound)
        {
            if (audioControl == null) audioControl = AudioControl.Instance;

            //Debug.Log("SimpleButton: click sound");
            audioControl.PlayClickSound(clickSoundIndex);
        }
    }
}
