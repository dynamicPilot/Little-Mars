﻿using LittleMars.AudioSystems;
using LittleMars.Commands;
using LittleMars.Commands.Level;
using LittleMars.Commands.MainMenu;
using LittleMars.Common.Catalogues;
using LittleMars.Common.Signals;
using LittleMars.Installers.Games;
using LittleMars.LevelMenus;
using LittleMars.Localization;
using LittleMars.MainMenus;
using LittleMars.SceneControls;
using LittleMars.UI.MainMenu;
using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using LittleMars.TimescaleControls;
using System;
using UnityEngine;
using Zenject;
using LittleMars.Settings;

namespace LittleMars.Installers
{
    public class MenuSceneInstaller : MonoInstaller<MenuSceneInstaller>
    {
        [Inject] Settings _settings = null;
        public override void InstallBindings()
        {
            InstallMenu();
            InstallCommands();
            InstallSounds();
            InstallSettings();
            InstallLocalization();
            InstallSignals();
            InstallWindowManager();

            InstallTimescale();
        }

        void InstallMenu()
        {
            Container.BindInterfacesAndSelfTo<MenuSceneControl>().AsSingle();
            Container.Bind<LevelsMenu>().AsSingle();
            Container.Bind<ISlotOnClick>().To<LevelsInfoSlotsByClick>().AsSingle();
            
        }

        void InstallTimescale()
        {
            Container.BindInterfacesAndSelfTo<TimescaleControl>().AsSingle();   
        }

        void InstallWindowManager()
        {
            WindowManagerInstaller.WindowPrefab = _settings.WindowPrefab;
            WindowManagerInstaller.Install(Container);
            //Container.BindInterfacesAndSelfTo<WindowManager>().AsSingle();
            //Container.Bind<WindowFactory>().AsSingle();

            //Container.BindFactory<WindowID, GameWindow, GameWindow.Factory>()
            //    .FromSubContainerResolve()
            //    .ByNewContextPrefab<GameWindowInstaller>(_settings.WindowPrefab);
        }

        void InstallCommands()
        {
            Container.Bind<MenuReceiver>().AsSingle();
            Container.Bind<SceneCommandManager>().To<MainMenuCommandManager>().AsSingle();
            Container.Bind<GameMenu>().AsSingle();
            Container.BindInterfacesAndSelfTo<ToLevelCommand>().AsSingle();
            Container.BindFactory<QuitCommand, QuitCommand.Factory>();
        }

        void InstallSounds()
        {
            Container.Bind<SoundsCatalogue>().AsSingle();
            Container.Bind<UISoundSystem>().AsSingle();
            Container.Bind<SoundsForGameMenuUI>().AsSingle();
        }

        void InstallSettings()
        {
            Container.Bind<SettingsMenu>().AsSingle();
        }

        void InstallLocalization()
        {
            Container.Bind<ILevelLangManager>().To<MenuLangsManager>().AsSingle();
        }

        void InstallSignals()
        {
            //SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ToLevelSignal>();
            Container.DeclareSignal<EndSceneSignal>();
            Container.DeclareSignal<NeedTextUpdateSignal>();
        }

        [Serializable]
        public class Settings
        {
            public GameObject WindowPrefab;
        }
    }
}
