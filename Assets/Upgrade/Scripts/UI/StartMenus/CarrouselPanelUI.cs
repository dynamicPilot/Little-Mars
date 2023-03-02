using LittleMars.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.StartMenus
{
    public class CarrouselPanelUI : MonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Button _prevButton;
        [SerializeField] Button _nextButton;
        [Header("Settings")]
        [SerializeField] SignCatalogueForUI _cataloque;

        protected int _index;
        bool _hasChanged;
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
            _hasChanged = false;
            CheckIndex();
            UpdateImage();
        }

        public void UpdateIndex(int index)
        {
            _index = index;
            OnIndexChanged();
        }

        public int GetIndex()
        {
            return _index;
        }

        public bool NeedSave()
        {
            return _hasChanged;
        }

        void Next()
        {
            _index++;
            OnButtonClick();
            OnIndexChanged();
        }

        void Prev()
        {
            _index--;
            OnButtonClick();
            OnIndexChanged();
        }

        protected virtual void OnIndexChanged()
        {
            _hasChanged = true;
            CheckIndex();
            UpdateImage();
        }

        protected virtual void OnButtonClick()
        {

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
