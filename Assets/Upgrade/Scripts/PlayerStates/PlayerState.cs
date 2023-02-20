using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.PlayerStates
{

    public class PlayerState : IPlayerState
    {
        int _level = 0;
        List<int> _completedLevels;
        public int GetLevelNumber() => _level;
        public int[] GetCompletedLevels() => _completedLevels.ToArray();
        public void ToNextLevel()
        {

        }
    }
}
