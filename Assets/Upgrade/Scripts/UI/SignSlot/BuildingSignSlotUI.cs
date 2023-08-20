using LittleMars.UI.Effects;
using UnityEngine;

namespace LittleMars.UI.SignSlot
{
    public class BuildingSignSlotUI : SignSlotUI
    {
        [SerializeField] private SizeUIEffect _size;

        public void SetSize(int index)
        {
            _size.SetSize(index);
        }

        public void HideSize()
        {
            _size.SetSize(0);
        }

    }
}
