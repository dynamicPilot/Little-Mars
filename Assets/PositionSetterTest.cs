using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetterTest : MonoBehaviour
{
    [SerializeField] RectTransform _target;
    [SerializeField] float _targetContainerScale;
    [SerializeField] Canvas _canvas;
    [SerializeField] float _offset;

    RectTransform _transform;
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        ToTargetUpCenter();
    }

    void ToTarget()
    {
        _transform.position = _target.position;
    }

    void ToTargetRightCorner()
    {
        var position = _target.position;

        var targetXMax = _target.rect.xMax * _targetContainerScale;
        var targetYMin = _target.rect.yMin * _targetContainerScale;

        var canvasHeight = _canvas.GetComponent<RectTransform>().rect.height;
        var canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;

        Debug.Log($"Canvas: width {canvasWidth} and height {canvasHeight}");

        var screenUnitsPerCanvasUnitHeight = Screen.height / canvasHeight;
        var screenUnitsPerCanvasUnitWidth = Screen.width / canvasWidth;

        Debug.Log($"Screen per Canvas: width {screenUnitsPerCanvasUnitWidth} and height {screenUnitsPerCanvasUnitHeight}");

        var deltaX = targetXMax * screenUnitsPerCanvasUnitHeight;
        var deltaY = targetYMin * screenUnitsPerCanvasUnitHeight;

        _transform.position = new Vector2(position.x + deltaX, position.y - deltaY);
    }

    void ToTargetUpCenter()
    {
        var position = _target.position;

        _targetContainerScale = _target.lossyScale.x / _canvas.GetComponent<RectTransform>().lossyScale.x;

        var targetXMax = _target.rect.xMax * _targetContainerScale;
        var targetYMin = _target.rect.yMin * _targetContainerScale;

        var canvasHeight = _canvas.GetComponent<RectTransform>().rect.height;
        var canvasWidth = _canvas.GetComponent<RectTransform>().rect.width;

        Debug.Log($"Canvas: width {canvasWidth} and height {canvasHeight}");
        Debug.Log($"Canvas: scale {_canvas.scaleFactor}");
        Debug.Log($"Canvas: lossy scale {_canvas.GetComponent<RectTransform>().lossyScale}");
        Debug.Log($"Canvas: local scale {_canvas.GetComponent<RectTransform>().localScale}");

        var screenUnitsPerCanvasUnitHeight = Screen.height / canvasHeight;
        var screenUnitsPerCanvasUnitWidth = Screen.width / canvasWidth;

        Debug.Log($"Screen per Canvas: width {screenUnitsPerCanvasUnitWidth} and height {screenUnitsPerCanvasUnitHeight}");

        var deltaX = targetXMax * screenUnitsPerCanvasUnitHeight;
        var deltaY = targetYMin * screenUnitsPerCanvasUnitHeight;

        _transform.position = new Vector2(position.x, position.y - deltaY);
    }

}
