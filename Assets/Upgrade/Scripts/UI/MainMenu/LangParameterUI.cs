using LittleMars.AudioSystems;
using LittleMars.Common.Signals;
using LittleMars.Configs;
using LittleMars.UI.StartMenus;
using Zenject;

namespace LittleMars.UI.MainMenu
{
    public class LangParameterUI : CarrouselPanelUI
    {
        LangSettings _settings;
        UISoundSystem _soundSystem;
        SignalBus _signalBus;

        [Inject]
        public void Constructor(SignalBus signalBus, LangSettings settings, UISoundSystem soundSystem)
        {
            _settings = settings;
            _signalBus = signalBus;
            _soundSystem = soundSystem;
        }

        public void ToDefault()
        {
            var defaultIndex = _settings.ToDefaultAndGetLang();
            UpdateIndex(defaultIndex);
        }

        protected override void OnButtonClick()
        {
            _soundSystem.PlayUISound(Common.UISoundType.clickThird);
        }

        protected override void OnIndexChanged()
        {
            base.OnIndexChanged();
            _settings.UpdateLang(_index);
            _signalBus.Fire<NeedTextUpdateSignal>();
        }

    }
}
