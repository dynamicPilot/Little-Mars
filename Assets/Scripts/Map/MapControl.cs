using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapControl : MonoBehaviour
{
    //[Header("Map Slots")]
    //[SerializeField] private Transform slotParent;
    //[SerializeField] private List<MapSlot> slots;

    [Header("ScrollView")]
    [SerializeField] private Scrollbar horizontalScrollbar;
    [SerializeField] private float startValueForHorizontal = 0.5f;
    [SerializeField] private Scrollbar verticalScrollbar;
    [SerializeField] private float startValueForVertucal = 0.3f;

    private MapCreator mapCreator;

    private void Awake()
    {
        mapCreator = GetComponent<MapCreator>();
    }

    public void CreateMapForLevel(LevelInfo levelInfo)
    {
        mapCreator.CreateMapForLevel(levelInfo);
        SetSliderToMiddle();
    }

    void SetSliderToMiddle()
    {
        horizontalScrollbar.value = startValueForHorizontal;
        verticalScrollbar.value = startValueForVertucal;
    }

    public float[] GetSlidersValue()
    {
        return new float[2] { horizontalScrollbar.value, verticalScrollbar.value };
    }

    public void SetSlidersValue(float[] values)
    {
        if (values.Length != 2)
        {
            SetSliderToMiddle();
            return;
        }

        Debug.Log("MapControl: set sliders value to " + values[0] + " and " + values[1]);
        horizontalScrollbar.value = values[0];
        verticalScrollbar.value = values[1];
        Debug.Log("MapControl: current sliders value is " + horizontalScrollbar.value + " and " + verticalScrollbar.value);
    }

    public int GetNumberOfEmptySlots()
    {
        int number = 0;

        foreach(MapSlot slot in mapCreator.Slots)
        {
            if (slot.IsEmpty)
            {
                number++;
            }
        }

        return number;
    }
}
