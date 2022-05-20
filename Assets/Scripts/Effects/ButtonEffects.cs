using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour
{
    [Header("Switch Sprite")]
    [SerializeField] private bool switchSpriteIsOn;
    [SerializeField] private bool spriteForInnerImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int defaultSpriteIndex = 0;

    [Header("Switch Color")]
    [SerializeField] private bool switchColorIsOn;
    [SerializeField] private bool colorForInnerImage;
    [SerializeField] private Color[] colors;
    [SerializeField] private int defaultColorIndex = 0;

    [Header("UI Elements")]
    [SerializeField] private Image innerImage;

    private Button button;
    private Image image;

    private List<int> currentCounters;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        //innerImage = GetComponentInChildren<Image>();

        currentCounters = new List<int>();
        currentCounters.Add(defaultSpriteIndex - 1);
        currentCounters.Add(defaultColorIndex - 1);
        //if (switchSpriteIsOn)
        //{
        //    currentCounters.Add(defaultSpriteIndex - 1);
        //}
        //else
        //{
        //    currentCounters.Add(-2);
        //}

        //if (switchColorIsOn)
        //{
        //    currentCounters.Add(defaultColorIndex - 1);
        //}
        //else
        //{
        //    currentCounters.Add(-2);
        //}

    }

    public void SetSpriteByIndex(int index)
    {
        if (index < 0 || index >= sprites.Length)
        {
            return;
        }

        currentCounters[0] = index;
        SetSprite();
    }

    public void SwitchSprite()
    {
        if (!switchSpriteIsOn)
        {
            return;
        }

        currentCounters[0] += 1;

        if (currentCounters[0] >= sprites.Length )
        {
            currentCounters[0] = 0;
        }

        SetSprite();
    }

    void SetSprite()
    {
        if (spriteForInnerImage && innerImage != null)
        {
            innerImage.sprite = sprites[currentCounters[0]];
        }
        else
        {
            image.sprite = sprites[currentCounters[0]];
        }
    }

    public void SetColorByIndex(int index)
    {
        if (index < 0 || index >= colors.Length)
        {
            return;
        }

        currentCounters[1] = index;
        SetColor();
    }

    public void SwitchColor()
    {
        if (!switchColorIsOn)
        {
            return;
        }

        currentCounters[1] += 1;

        if (currentCounters[1] >= colors.Length)
        {
            currentCounters[1] = 0;
        }

        SetColor();
    }

    void SetColor()
    {
        if (colorForInnerImage && innerImage != null)
        {
            innerImage.color = colors[currentCounters[1]];
        }
        else
        {
            image.color = colors[currentCounters[1]];
        }
    }
}
