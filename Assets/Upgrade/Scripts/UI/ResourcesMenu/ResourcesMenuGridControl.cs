using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.ResourcesMenu
{
    public class ResourcesMenuGridControl: MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] RectTransform _container;
        [SerializeField] GridLayoutGroup _layout;

        [Header("Cell Params")]
        [SerializeField] float _cellWidth = 60;
        [SerializeField] float _padding = 21;

        public void SetWidth(int cellCount)
        {
            var oneLineWidth = _padding * 2 + _cellWidth * cellCount;
            var containerWidth = _container.rect.width;

            if (oneLineWidth > containerWidth)
            {
                var columnNumber = Mathf.FloorToInt((containerWidth - _padding * 2) / _cellWidth);
                _layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _layout.constraintCount = columnNumber;
            }
            else
            {
                _layout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                _layout.constraintCount = 1;
            }
        }
    }
}
