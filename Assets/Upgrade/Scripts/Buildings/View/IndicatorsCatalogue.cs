using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    [CreateAssetMenu(menuName = "LittleMars/IndicatorSpriteCatalogue")]
    public class IndicatorsCatalogue : ScriptableObject
    {
        [SerializeField] private Sprite _on;
        [SerializeField] private Sprite _off;
        [SerializeField] private Sprite _paused;


        public Sprite GetIndicator(BStates state)
        {
            if (state == BStates.off) return _off;
            else if (state == BStates.on) return _on;
            else return _paused;
        }
    }
}
