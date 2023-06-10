using LittleMars.UI;
using LittleMars.UI.Windows;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace LittleMars.WindowManagers
{
    public class WindowFactory
    {
        GameWindow.Factory _factory;

        public WindowFactory(GameWindow.Factory factory)
        {
            _factory = factory;
        }

        public GameWindow Create(WindowID id, RectTransform container)
        {
            var window = _factory.Create(id);
            SetTransform(window.transform, container);
            return window;
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
