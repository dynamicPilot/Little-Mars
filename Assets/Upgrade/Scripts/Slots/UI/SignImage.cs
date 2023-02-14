using LittleMars.Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.Slots.UI
{
    public class SignImage : MonoBehaviour
    {
        [SerializeField] Image _image;

        IconsCatalogue _iconsCatalogue;

        [Inject]
        public void Constructor(IconsCatalogue iconsCatalogue)
        {
            _iconsCatalogue = iconsCatalogue;
        }

        public void Resource(Resource resource)
        {
            var icon = GetIcon(resource);
            SetIcon(icon);
        }

        Sprite GetIcon(Resource resource)
        {
            var icon = _iconsCatalogue.FieldTypeIcon(resource);
            icon = icon != null ? icon : _iconsCatalogue.ResourceIcon(resource);
            return icon;
        }

        void SetIcon(Sprite icon)
        {
            _image.sprite = icon;
        }

        public class Factory: PlaceholderFactory<SignImage>
        {

        }
    }
}
