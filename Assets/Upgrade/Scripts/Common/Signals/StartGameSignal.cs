using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Common.Signals
{
    public struct StartGameSignal
    {
    }

    public struct GameOverSignal
    {
        public int GoalIndex;
        public bool IsStaff;
    }

    public struct EndGameSignal
    {

    }
}
