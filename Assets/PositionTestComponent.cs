using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTestComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Screen: width {Screen.width} and height { Screen.height}");
        Debug.Log($"TEST {gameObject.name}: rect position {GetComponent<RectTransform>().position}");
        Debug.Log($"TEST {gameObject.name}: local position {GetComponent<RectTransform>().localPosition}");
        Debug.Log($"TEST {gameObject.name}: anchored position {GetComponent<RectTransform>().anchoredPosition}");
        Debug.Log($"TEST {gameObject.name}: rect height {GetComponent<RectTransform>().rect.height}");
        Debug.Log($"TEST {gameObject.name}: rect width {GetComponent<RectTransform>().rect.width}");
        Debug.Log($"TEST {gameObject.name}: rect x {GetComponent<RectTransform>().rect.x}, y {GetComponent<RectTransform>().rect.y}");
        Debug.Log($"TEST {gameObject.name}: rect xMin {GetComponent<RectTransform>().rect.xMin}, xMax {GetComponent<RectTransform>().rect.xMax}");
        Debug.Log($"TEST {gameObject.name}: pivot {GetComponent<RectTransform>().pivot}");
        Debug.Log($"TEST {gameObject.name}: lossy scale {GetComponent<RectTransform>().lossyScale}");
        Debug.Log($"TEST {gameObject.name}: local scale {GetComponent<RectTransform>().localScale}");
    }
}
