using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEffects : MonoBehaviour
{
    [SerializeField] private Color placementColor;
    [SerializeField] private Sprite placementSprite;
    [SerializeField] private Image image;
    
    //private RectTransform rectTransform;
    

    private Color defaultColor;
    private Sprite defaultSprite;

    private void Awake()
    {
        //rectTransform = GetComponent<RectTransform>();
        //image = GetComponent<Image>();
    }

    public void ShowBuildingPlacement(bool needToSetActive = true)
    {
        if (needToSetActive)
            image.gameObject.SetActive(true);

        //Debug.Log("SlotEffects: make effect");
        defaultSprite = image.sprite;
        defaultColor = image.color;

        image.sprite = placementSprite;
        image.color = placementColor;
    }

    public void ReturnToDefault(bool needToSetActive = true)
    {
        if (needToSetActive)
            image.gameObject.SetActive(false);

        image.sprite = defaultSprite;
        image.color = defaultColor;
    }
}
