using LittleMars.Animations;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public class BuildingAnimationsControl : MonoBehaviour
    {
        [SerializeField] private BlinkingLightAnimation[] _lightAnimations;

        public void UpdateState(BStates state)
        {
            UpdateLights(state);               
        }

        private void UpdateLights(BStates state)
        {
            for (int i = 0; i < _lightAnimations.Length; i++)
            {
                if (state == BStates.on)
                    _lightAnimations[i].Play();
                else
                    _lightAnimations[i].Pause();
            }
        }
    }
}
