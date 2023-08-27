using LittleMars.UI.Windows;
using UnityEngine;

namespace LittleMars.TooltipSystem
{
    public class CanvasUtils
    {
        readonly RectTransform _canvasRectTransform;

        public CanvasUtils(SceneWindows sceneWindow)
        {
            _canvasRectTransform = sceneWindow.Canvas;
        }

        public float GetScreenUnitsPerCanvasUnit()
        {
            return Screen.height / _canvasRectTransform.rect.height;
        }

        public float GetCanvasScale()
        {
            return _canvasRectTransform.localScale.x;
        }
    }
}
