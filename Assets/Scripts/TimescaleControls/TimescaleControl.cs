using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.TimescaleControls
{
    public class TimescaleControl : IInitializable
    {
        readonly Settings _settings;

        public TimescaleControl(Settings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            Time.timeScale = _settings.InitialTimescale;
            Debug.Log("TimeControl");

        }

        [Serializable]
        public class Settings
        {
            public float InitialTimescale;
        }
    }
}
