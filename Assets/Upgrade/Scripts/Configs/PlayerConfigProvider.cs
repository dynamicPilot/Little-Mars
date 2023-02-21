namespace LittleMars.Configs
{
    public class PlayerConfigProvider
    {
        readonly PlayerSettings _settings;

        public PlayerConfigProvider(PlayerSettings settings)
        {
            _settings = settings;
        }

        public PlayerConfig GetData()
        {
            return new PlayerConfig(_settings.IsMusicOn, _settings.MusicVolume,
                _settings.IsSoundOn, _settings.SoundVolume, _settings.Lang);
        }
    }
}
