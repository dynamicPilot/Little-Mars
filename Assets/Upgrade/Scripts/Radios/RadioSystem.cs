using Zenject;

namespace LittleMars.Radios
{
    public class RadioSystem : IInitializable
    {
        readonly RadioUI.Factory _factory;

        RadioUI _radioUI;

        public RadioSystem(RadioUI.Factory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            CreateRadio();
            StartRadio();
        }

        void CreateRadio()
        {
            _radioUI = _factory.Create();
        }

        void StartRadio()
        {
            _radioUI.StartRadio();
        }

    }
}
