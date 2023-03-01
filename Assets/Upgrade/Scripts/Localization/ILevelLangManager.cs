using LittleMars.Common;

namespace LittleMars.Localization
{
    public interface ILevelLangManager
    {
        string GetText(string tag, TagGroup group);
    }
}
