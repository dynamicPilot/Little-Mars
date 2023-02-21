using LittleMars.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.StartMenus
{
    public class CarrouselPanelUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;
        [Header("Settings")]
        [SerializeField] SignCatalogueForUI _cataloque;

        protected int _index;

        private void OnEnable()
        {
            SetListeners();
        }

        private void OnDisable()
        {
            _prevButton.onClick.RemoveAllListeners();
            _nextButton.onClick.RemoveAllListeners();
        }

        void SetListeners()
        {
            _prevButton.onClick.AddListener(Prev);
            _nextButton.onClick.AddListener(Next);
        }

        public void SetIndex(int index)
        {
            _index = index;
            CheckIndex();
            UpdateImage();
        }

        public int GetIndex()
        {
            return _index;
        }

        void Next()
        {
            _index++;
            CheckIndex();
        }

        void Prev()
        {
            _index--;
            CheckIndex();
        }

        void CheckIndex()
        {
            if (_index < 0) _index = _cataloque.Sprites.Length - 1;
            else if (_index >= _cataloque.Sprites.Length) _index = 0;
        }

        void UpdateImage()
        {
            _image.sprite = _cataloque.Sprites[_index];
        }

    }
}
