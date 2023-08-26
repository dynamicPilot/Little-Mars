using LittleMars.Common;
using LittleMars.TooltipSystem;
using LittleMars.WindowManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Direction = LittleMars.Common.Direction;

namespace LittleMars.UI.Tooltip
{
    public class TooltipPositionSetter : MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;

        float _width = 0f;
        float _height = 0f;

        private void Awake()
        {
            _width = _rectTransform.rect.width;
            _height = _rectTransform.rect.height;
        }

        public bool TrySetTooltip(TooltipContext context)
        {
            if (!CheckContext(context)) return false;

            SetTooltipPosition(context.Position, context.Direction);
            return true;
        }

        void SetTooltipPosition(Vector2 position, Direction direction)
        {
            Debug.Log("Tooltip: position from context: " + position.x + " " + position.y + ". Direction " + direction);
            Debug.Log("Tooltip: width: " + _width + " height " + _height);

            var xPos = position.x;
            var yPos = position.y;

            //if (direction == Direction.left) xPos -= _width / 2;
            //else if (direction == Direction.right) xPos += _width / 2;
            //else if (direction == Direction.up) yPos -= _height / 2;
            //else if (direction == Direction.down) yPos += _height / 2;

            _rectTransform.localPosition = new Vector2 (position.x, position.y);
            //_rectTransform.position = new Vector2(xPos, yPos);
        }

        bool CheckContext(TooltipContext context)
        {
            // add check for text
            return true;
        }


    }
}
