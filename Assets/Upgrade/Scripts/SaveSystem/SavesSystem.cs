using Zenject;

namespace LittleMars.SaveSystem
{
    public class SavesSystem
    {
        readonly ISaver _saver;
        readonly ILoader _loader;
        public SavesSystem(ISaver saver, ILoader loader)
        {
            _saver = saver;
            _loader = loader;
        }

        public void SaveData()
        {
            _saver.SaveData();
        }

        public PlayerData LoadData()
        {
            return _loader.LoadData();
        }

        public class Factory : PlaceholderFactory<ISaver, ILoader, SavesSystem>
        { }
    }
}
