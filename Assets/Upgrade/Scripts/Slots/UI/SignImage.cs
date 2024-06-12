using LittleMars.Common;
using LittleMars.Common.Catalogues;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Resource = LittleMars.Common.Resource;

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

        public void IsBlocked()
        {
            var icon = GetIsBlockedIcon();
            SetIcon(icon);
        }

        public void Resource(Resource resource)
        {
            var icon = GetResourceIcon(resource);
            SetIcon(icon);
        }

        Sprite GetIsBlockedIcon()
        {
            return _iconsCatalogue.SlotIsBlockedIcon();
        }

        Sprite GetResourceIcon(Resource resource)
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
