using LittleMars.SaveSystem;

namespace LittleMars.Common.Signals
{
    public struct DataIsLoadedSignal
    {
        public PlayerData PlayerData { get; set; }
    }

    public struct ConfigIsLoadedSignal
    {
        public PlayerConfig PlayerConfig { get; set; }
        public bool NeedUpdate { get; set; }
    }

    public struct NoConfigIsLoadedSignal
    {
        public PlayerConfig PlayerConfig { get; set; }
    }

    public struct StartLoadingSignal
    { }

    public struct NeedSaveConfigSignal
    { }
}
