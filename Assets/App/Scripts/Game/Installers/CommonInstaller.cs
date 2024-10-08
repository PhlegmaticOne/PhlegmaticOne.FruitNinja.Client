﻿using App.Scripts.Game.Features._Boot;
using App.Scripts.Game.Features.Blocks.Services;
using App.Scripts.Game.Features.BlocksSplit.Factories;
using App.Scripts.Game.Features.Common;
using App.Scripts.Game.Features.Difficulty.Services;
using App.Scripts.Game.Features.Packages.Services;
using App.Scripts.Game.Features.Spawning.Factories;
using App.Scripts.Game.Infrastructure.Input;
using App.Scripts.Game.Infrastructure.Session;
using App.Scripts.Game.Modes.Base;
using App.Scripts.Game.Modes.ByBlocks;
using App.Scripts.Game.Modes.ByLifes;
using App.Scripts.Game.Modes.ByScore;
using App.Scripts.Game.Modes.ByTime;
using App.Scripts.Game.States;
using UnityEngine;
using Zenject;

namespace App.Scripts.Game.Installers {
    public class CommonInstaller : MonoInstaller {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameBootstrap _gameBootstrap;

        [SerializeField] private GameModeByScore _gameModeByScore;
        [SerializeField] private GameModeByBlocks _gameModeByBlocks;
        [SerializeField] private GameModeByTime _gameModeByTime;
        [SerializeField] private GameModeByLifes _gameModeByLifes;
        
        public override void InstallBindings() {
            BindCameraProvider();
            BindInputSystem();
            BindBlockService();
            BindDifficulty();
            BindNetworkSession();
            BindStates();
            BindBootstrap();
            BindGameModes();
        }

        private void BindGameModes()
        {
            Container.Bind<IGameMode>().To<GameModeByScore>().FromInstance(_gameModeByScore).AsSingle();
            Container.Bind<IGameMode>().To<GameModeByBlocks>().FromInstance(_gameModeByBlocks).AsSingle();
            Container.Bind<IGameMode>().To<GameModeByTime>().FromInstance(_gameModeByTime).AsSingle();
            Container.Bind<IGameMode>().To<GameModeByLifes>().FromInstance(_gameModeByLifes).AsSingle();
            Container.Bind<IGameModeProvider>().To<GameModeProvider>().AsSingle();
        }

        private void BindBootstrap() {
            Container.BindInterfacesTo<GameBootstrap>().FromInstance(_gameBootstrap).AsSingle();
        }

        private void BindNetworkSession() {
            Container.Bind<INetworkSession>().To<NetworkSession>().AsSingle();
        }

        private void BindStates() {
            Container.Bind<StateStartGame>().AsSingle();
            Container.Bind<StateEndGame>().AsSingle();
        }

        private void BindDifficulty() {
            Container.Bind<ISpawningDifficulty>().To<SpawningDifficultyDefault>().AsSingle();
            Container.Bind<IPackageGenerator>().To<PackageGenerator>().AsSingle();
        }

        private void BindBlockService() {
            Container.Bind<IBlockContainer>().To<BlockContainer>().AsSingle();
            Container.Bind<IBlockFactory>().To<BlockFactory>().AsSingle();
            Container.Bind<IBlockSplitter>().To<BlockSplitter>().AsSingle();
        }

        private void BindInputSystem() {
            Container.Bind<IInputSystemFactory>().To<InputSystemFactory>().AsSingle();
        }

        private void BindCameraProvider() {
            Container.Bind<CameraProvider>().FromInstance(new CameraProvider(_camera)).AsSingle();
        }
    }
}