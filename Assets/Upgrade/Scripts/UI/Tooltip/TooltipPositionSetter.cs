using LittleMars.TooltipSystem;
using UnityEngine;
using Zenject;
using Direction = LittleMars.Common.Direction;

namespace LittleMars.UI.Tooltip
{
    public class TooltipPositionSetter : MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;

        CanvasUtils _utils;

        float _width = 0f;
        float _height = 0f;

        [Inject]
        public void Constructor(CanvasUtils utils)
        {
            _utils = utils;
            Init();
        }

        void Init()
        {
            _width = _rectTransform.rect.width;
            _height = _rectTransform.rect.height;
        }

        public bool TrySetTooltip(TooltipContext context)
        {
            if (!CheckContext(context)) return false;

            CheckTargetTransform(context.Target);
            SetTooltipPosition(context);
            return true;
        }

        void SetTooltipPosition(TooltipContext context)
        {
            var position = context.Target.position;
            var target = context.Target;

            var scaleX = target.lossyScale.x / _utils.GetCanvasScale();
            var scaleY = target.lossyScale.y / _utils.GetCanvasScale();

            var screenUnitsPerCanvasUnit = _utils.GetScreenUnitsPerCanvasUnit();

            Debug.Log($"Scale X: {scaleX}");

            var targetXMax = target.rect.xMax * scaleX;
            var targetXMin = target.rect.xMin * scaleX;

            var targetYMin = target.rect.yMin * scaleY;
            var targetYMax = target.rect.yMax * scaleY;

            var targetHeight = target.rect.height * scaleY;
            var targetWidth = target.rect.width * scaleX;

            var targetPivotX = target.pivot.x;
            var targetPivotY = target.pivot.y;

            var offset = context.Offset;

            // align tooltip rect to center of target
            var deltaX = targetXMin * (0.5f - targetPivotX) + targetXMax * (0.5f - targetPivotX);
            var deltaY = targetYMin * (targetPivotY - 0.5f) + targetYMax * (targetPivotY - 0.5f);

            //Debug.Log($"Direction: {context.Direction}");
            //Debug.Log($"Target: xMin {targetXMin} and xMax {targetXMax}");
            //Debug.Log($"Target: yMin {targetYMin} and yMax {targetYMax}");
            //Debug.Log($"Target: height {targetHeight} and width {targetWidth}");
            //Debug.Log($"Target: position {target.position}");

            //Debug.Log($"Tooltip: target position {new Vector2(position.x + targetWidth / 2, position.y)}");

            //position = new Vector2(position.x + (deltaX * screenUnitsPerCanvasUnit), position.y + (deltaY * screenUnitsPerCanvasUnit));

            if (context.Direction == Direction.up || context.Direction == Direction.down)
            {
                offset *= scaleY;

                if (context.Direction == Direction.up) deltaY += (offset + _height /2f + targetHeight / 2f);
                else deltaY -= (offset + _height / 2f + targetHeight / 2f);
            }
            else
            {
                offset *= scaleX;

                if (context.Direction == Direction.left) deltaX -= (offset + _width / 2f + targetWidth / 2f);
                else deltaX += (offset + _width / 2f + targetWidth / 2f);
            }

            _rectTransform.position = new Vector2(position.x + (deltaX * screenUnitsPerCanvasUnit), position.y + (deltaY * screenUnitsPerCanvasUnit));
        }

        void CheckTargetTransform(RectTransform target)
        {
            if (target.pivot.x != 0.5f || target.pivot.y != 0.5f)
                Debug.Log($"Tooltip target with NON center pivot {target.pivot}");
        }

        bool CheckContext(TooltipContext context)
        {
            // add check for text
            return true;
        }


    }
}
