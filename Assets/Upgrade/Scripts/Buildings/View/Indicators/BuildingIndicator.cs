using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View.Indicators
{
    public abstract class BuildingIndicator : MonoBehaviour
    {
        public abstract void UpdateIndicator(BStates state);
    }
}
