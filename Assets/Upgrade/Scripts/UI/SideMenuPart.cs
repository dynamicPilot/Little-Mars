using LittleMars.Common;
using LittleMars.Common.Signals;
using UnityEngine;
using Zenject;

namespace LittleMars.UI
{
    public class SideMenuPart
    {
        //readonly protected SignalBus _signalBus;

        //public SideMenuPart (SignalBus signalBus)
        //{
        //    _signalBus = signalBus;
        //}

        public virtual void OnOpenMenu()
        {
            UpdateSlots();
            //SubscribeToUpdate();
        }

        //public virtual void OnCloseMenu()
        //{
        //    //UnsubscribeToUpdate();
        //}

        //protected virtual void SubscribeToUpdate()
        //{ }

        //protected virtual void UnsubscribeToUpdate()
        //{ }

        public virtual void UpdateSlots()
        { }

        public virtual void CreateSlots(RectTransform transform)
        { }
    }
}
