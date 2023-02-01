using LittleMars.Common.Catalogues;
using UnityEngine;
using UnityEngine.UI;

namespace LittleMars.UI.Buttons
{
    public class SignByIndexUI : MonoBehaviour
    {
        [SerializeField] private Image _sign;
        [SerializeField] private SignCatalogueForUI _catalogue;
        [Header("Options")]
        [SerializeField] private bool _hideOnAwake = false;

        private void Awake()
        {
            if (_hideOnAwake) _sign.enabled = false;
        }

        public void UpdateSign(int index)
        {
            if (!_sign.enabled) _sign.enabled = true;
            _sign.sprite = GetSpriteFromCatalogue(index);
        }

        private Sprite GetSpriteFromCatalogue(int index)
        {
            if (index < _catalogue.Sprites.Length) return _catalogue.Sprites[index];
            else return _catalogue.DefaultSprite;
        }

    }
}
