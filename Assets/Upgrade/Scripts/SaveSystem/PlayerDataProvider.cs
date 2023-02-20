using LittleMars.PlayerStates;
using UnityEngine;

namespace LittleMars.SaveSystem
{
    public class PlayerDataProvider
    {
        readonly IPlayerState _playerState;

        public PlayerData GetData()
        {
            if (_playerState == null) return null;
            return FormData();

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
