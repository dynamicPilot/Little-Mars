using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View.Indicators
{
    [CreateAssetMenu(menuName = "LittleMars/IndicatorSpriteCatalogue")]
    public class IndicatorsCatalogue : ScriptableObject
    {
        [SerializeField] private Sprite _on;
        [SerializeField] private Sprite _off;
        [SerializeField] private Sprite _paused;
        [SerializeField] private Sprite _effected;


        public Sprite GetIndicator(BStates state)
        {
            if (state == BStates.off) return _off;
            else if (state == BStates.on) return _on;
            else if (state == BStates.paused) return _paused;
            else return _effected;
        }
    }
}
