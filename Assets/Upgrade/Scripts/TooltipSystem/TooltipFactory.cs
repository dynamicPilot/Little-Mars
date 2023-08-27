using LittleMars.UI.Tooltip;
using UnityEngine;

namespace LittleMars.TooltipSystem
{
    public class TooltipFactory
    {
        readonly TooltipObject.Factory _factory;

        public TooltipFactory(TooltipObject.Factory factory)
        {
            _factory = factory;
        }

        public TooltipObject Create(RectTransform container)
        {
            var tooltip = _factory.Create();
            SetTransform(tooltip.transform, container);
            return tooltip;
        }


        void SetTransform(Transform transform, RectTransform container)
        {
            transform.SetParent(container);
            transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
            // add checking of the index
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);

            var rectTransform = transform.GetComponent<RectTransform>();
            if (rectTransform == null) return;

            rectTransform.offsetMin = new Vector2(0f, 0f);
            rectTransform.offsetMax = new Vector2(0f, 0f);
        }
    }
}
