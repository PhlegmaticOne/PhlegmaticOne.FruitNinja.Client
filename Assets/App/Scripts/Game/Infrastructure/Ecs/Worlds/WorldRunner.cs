﻿using System.Collections.Generic;
using App.Scripts.Game.Infrastructure.Ecs.Systems;
using UnityEngine;
using Zenject;

namespace App.Scripts.Game.Infrastructure.Ecs.Worlds {
    public class WorldRunner : MonoBehaviour {
        [SerializeField] private bool _isRunGame;
        
        private World _world;
        private bool _isConstructed;

        [Inject]
        private void Construct(World world, IEnumerable<ISystem> systems) {
            _world = world;
            _world.Initialize(systems);
        }

        public void Run() {
            if (!_isRunGame) {
                return;
            }
            
            _world.Construct();
            _isConstructed = true;
        }
        
        private void Update() {
            if (!IsRun()) {
                return;
            }
            
            _world.Update(Time.deltaTime);
        }

        private void FixedUpdate() {
            if (!IsRun()) {
                return;
            }
            
            _world.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnApplicationQuit() {
            if (!IsRun()) {
                return;
            }
            
            _world.Dispose();
        }

        private bool IsRun() => _isRunGame && _isConstructed;
    }
}