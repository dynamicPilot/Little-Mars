﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.PlayerStates
{

    public class PlayerState : IPlayerState
    {
        int _level = 0;

        public int GetLevelNumber() => _level;

        public void ToNextLevel()
        {

        }
    }
}