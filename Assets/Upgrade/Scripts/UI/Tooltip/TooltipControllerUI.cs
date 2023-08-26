using LittleMars.Common;
using LittleMars.TooltipSystem;
using LittleMars.WindowManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.Tooltip
{
    public class TooltipControllerUI: MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] RectTransform _windowRectTransform;
        [SerializeField] Button _button;

        [Header ("Tooltip Settings")]
        [SerializeField] Direction _direction;
        [SerializeField] float _offset;

        TooltipController _controller;

        [Inject]
        public void Constructor(TooltipController controller, RectTransform windowRectTransform)
        {
            _controller = controller;
            _windowRectTransform = windowRectTransform;
            Init();
        }

        void Init()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        void OnButtonClick()
        {
            var context = GetTooltipContext();
            _controller.CallTooltip(context);
        }


        Vector2 CalculateTooltipPostion()
        {
            var position = _rectTransform.localPosition;
            var height = _rectTransform.rect.height;
            var width = _rectTransform.rect.width;
            _offset = 0;

            Debug.Log("Tooltip width  : " + width + " height " + height + " position " + position.x + " " + (position.x + width).ToString());

            //if (_direction == Direction.left || _direction == Direction.right)
            //{
            //    position.y += height / 2;
            //    if (_direction == Direction.right) position.x += width + _offset;
            //    else position.x -= _offset;
            //}
            //else if (_direction == Direction.up || _direction == Direction.down)
            //{
            //    position.x += width / 2;
            //    if (_direction == Direction.down) position.y += width + _offset;
            //    else position.y -= _offset;
            //}

            //Debug.Log("Tooltip width  : " + width + " height " + height);
            return new Vector2 (Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y + _windowRectTransform.rect.height / 2));
        }

        TooltipContext GetTooltipContext()
        {
            var position = CalculateTooltipPostion();

            return new TooltipContext(_direction, position, "");
        }
    }
}
