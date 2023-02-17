using LittleMars.Common.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public interface ISlotOnClick
    {
        void SlotOnClick(int levelIndex);
    }
    public class LevelsInfoSlotsByClick : ISlotOnClick
    {
        readonly SignalBus _signalBus;
        public LevelsInfoSlotsByClick(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void SlotOnClick(int levelIndex)
        {
            if (levelIndex == -1) return;

            _signalBus.Fire(new ToLevelSignal
            {
                Index = levelIndex
            });
        }
    }
}
