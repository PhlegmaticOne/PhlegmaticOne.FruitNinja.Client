﻿using System;
using System.Linq;
using App.Scripts.Game.Features.Blocks.Models;
using App.Scripts.Game.Features.Spawning.Components;
using App.Scripts.Game.Features.Spawning.Configs;
using App.Scripts.Game.Features.Spawning.Configs.Blocks;
using App.Scripts.Game.Features.Spawning.Configs.Spawners;
using App.Scripts.Game.Infrastructure.Ecs.Components;
using App.Scripts.Game.Infrastructure.Ecs.Filters;
using App.Scripts.Game.Infrastructure.Ecs.Systems;
using App.Scripts.Game.Infrastructure.Random;
using UnityEngine;

namespace App.Scripts.Game.Features.Spawning.Systems {
    public class SystemSpawnByTime : SystemBase {
        private readonly SpawnersConfiguration _spawnersConfiguration;
        private readonly SpawnSystemConfiguration _spawnSystemConfiguration;

        private const float Gravity = 8;
        
        private IComponentsFilter _filter;

        public SystemSpawnByTime(
            SpawnersConfiguration spawnersConfiguration, 
            SpawnSystemConfiguration spawnSystemConfiguration) {
            _spawnersConfiguration = spawnersConfiguration;
            _spawnSystemConfiguration = spawnSystemConfiguration;
        }
        
        public override void OnAwake() {
            World.CreateEntity()
                .WithComponent(new ComponentTimer {
                    CurrentTime = 0,
                    Time = 5
                })
                .WithComponent(new ComponentSpawner());

            _filter = ComponentsFilter.Builder
                .With<ComponentSpawner>()
                .With<ComponentTimer>()
                .Build();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (var entity in _filter.Apply(World)) {
                var componentTimer = entity.GetComponent<ComponentTimer>();

                if (!componentTimer.IsEnd) {
                    continue;
                }

                var spawnerData = _spawnersConfiguration.GetRandomSpawnerData();
                var fruit = _spawnSystemConfiguration.FruitSpawnData.GetRandomItemBasedOnProbabilities();
                
                World.AppendEntity()
                    .WithComponent(new ComponentSpawnBlockData {
                        Acceleration = Gravity * Vector3.down,
                        Position = spawnerData.GetSpawnPoint(),
                        Speed = spawnerData.GetInitialSpeed(),
                        BlockType = fruit.Key,
                        BlockId = Guid.NewGuid()
                    });
                componentTimer.CurrentTime = 0;
            }
        }
    }
}