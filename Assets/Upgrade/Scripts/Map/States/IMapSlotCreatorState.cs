using System;
using System.Collections.Generic;

namespace LittleMars.Map.States
{
    public enum MapMode
    {
        auto,
        custom,
        none
    }

    public interface IMapSlotCreatorState : IDisposable
    {
        List<List<MapSlotExtended>> Create();
    }
}
