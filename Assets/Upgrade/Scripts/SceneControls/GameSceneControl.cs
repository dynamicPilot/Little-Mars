using LittleMars.PlayerStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.SceneControls
{
    public class GameSceneControl
    {
        readonly IPlayerState _player;

        public GameSceneControl(IPlayerState player)
        {
            _player = player;
        }


    }
}
