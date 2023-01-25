using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public abstract class BuildingIndicator : MonoBehaviour
    {
        public abstract void UpdateIndicator(BStates state);
    }
}
