namespace LittleMars.PlayerStates
{
    public interface IPlayerState
    {
        int GetLevelNumber();
        int[] GetCompletedLevels();
        void ToNextLevel();
    }
}
