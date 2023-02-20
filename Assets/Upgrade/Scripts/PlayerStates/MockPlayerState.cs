namespace LittleMars.PlayerStates
{
    public class MockPlayerState : IPlayerState
    {
        public int[] GetCompletedLevels()
        {
            return new int[1] { 0 };
        }

        public int GetLevelNumber()
        {
            return 0;
        }

        public void ToNextLevel()
        {

        }
    }
}
