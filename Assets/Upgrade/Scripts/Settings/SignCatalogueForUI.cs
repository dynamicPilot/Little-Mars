using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/SignCatalogueForUI")]
    public class SignCatalogueForUI : ScriptableObject
    {
        public Sprite DefaultSprite;
        public Sprite[] Sprites;
    }
}
