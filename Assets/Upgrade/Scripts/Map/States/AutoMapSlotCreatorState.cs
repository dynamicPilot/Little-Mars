using LittleMars.Common;
using LittleMars.Map.Helpers;
using LittleMars.Models.Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Map.States
{
    public class AutoMapSlotCreatorState : MapSlotCreatorState, IMapSlotCreatorState
    {
        public AutoMapSlotCreatorState(MapManager.Settings mapSettings,FieldManager fieldManager) 
            : base (mapSettings, fieldManager)
        {
        }

        public void Dispose()
        {            
        }

        public class Factory : PlaceholderFactory<AutoMapSlotCreatorState>
        {
        }
    }
}
