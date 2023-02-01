using UnityEngine;

namespace LittleMars.Common.Catalogues
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/SignCatalogueForUI")]
    public class SignCatalogueForUI : ScriptableObject
    {
        public Sprite DefaultSprite;
        public Sprite[] Sprites;
    }
}
