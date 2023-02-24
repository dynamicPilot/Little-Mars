using LittleMars.PlayerStates;
using UnityEngine;

namespace LittleMars.SaveSystem
{
    public class PlayerDataProvider
    {
        readonly IPlayerState _playerState;

        public PlayerDataProvider(IPlayerState playerState)
        {
            _playerState = playerState;
        }

        public bool GetData(out PlayerData data)
        {
            data = null;
            if (_playerState == null) return false;
            else if (!_playerState.NeedSave()) return false;
            else
            {
                data = FormData();
                return true;
            }
        }

        public PlayerData GetEmptyData()
        {
            return new PlayerData();
        }

        public PlayerData GetPlayerDataByGameData(GameData gameData)
        {
            return new PlayerData(gameData.CurrentLevelIndex, gameData.CompletedLevels);
        }

        PlayerData FormData()
        {
            return new PlayerData(_playerState.GetLevelNumber(), _playerState.GetCompletedLevels());
        }
    }
}
