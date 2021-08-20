using System;
using System.Collections.Generic;
using DI;
using Game.Actors.Ship;
using Game.Interfaces;
using Game.Systems;
using Game.Systems.Characters;
using Game.Systems.Sea;
using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class GameManager : BaseComponent
    {
        public static event Action GizmosEvent;
        public static GameProperties Properties => current.properties;
        public static GameManager current { get; set; }

        [SerializeField] private GameProperties properties;
 
        private static List<IGameSystem> systems = new List<IGameSystem>();
        private static List<IGameUpdateSystem> updateSystems = new List<IGameUpdateSystem>();
        private static List<IGameLateUpdateSystem> lateUpdateSystems = new List<IGameLateUpdateSystem>();
        private static List<IGamePhysicsSystem> physicsSystems = new List<IGamePhysicsSystem>();
        
        
        private static GenericMap<IGameSystem> systemsMap = new GenericMap<IGameSystem>();

        private readonly DependencyContainer diContainer = new DependencyContainer();

        private static T AddSystem<T>(T system) where T : IGameSystem
        {
            systems.Add(system);
            systemsMap.Set(system);
            if (system is IGameUpdateSystem updateSystem) updateSystems.Add(updateSystem);
            if (system is IGameLateUpdateSystem lateUpdateSystem) lateUpdateSystems.Add(lateUpdateSystem);
            if (system is IGamePhysicsSystem physicsSystem) physicsSystems.Add(physicsSystem);
            
            return system;
        }

        public ShipActor currentShip;

        private void Awake()
        {
            diContainer.Register(AddSystem(new WindSystem()));
            diContainer.Register(AddSystem(new AiCharacterSystem()));
            diContainer.Register(AddSystem(new GameCharacterSystem()));
            diContainer.Register(AddSystem(new ShipModelSystem()));
            diContainer.Register(AddSystem(new ShipActorsSystem()));
            diContainer.Register(AddSystem(new UserControlSystem()));
            diContainer.Register(AddSystem(new UserCharacterHud()));
            diContainer.Register(AddSystem(new LivingAreaSystem()));
            diContainer.Register(AddSystem(new ShipsCrewSystem()));
            diContainer.Register(AddSystem(new ObjectSpawnSystem()));
            
            diContainer.InjectDependencies();
            current = this;
            systems.Foreach(system=> system.Init());
        }

        public T Get<T>() => diContainer.GetItem<T>();

        private void Start()
        {
            systems.Foreach(system=> system.Start());
        }


        private void Update()
        {
            updateSystems.Foreach(system => system.Update());
        }

        private void FixedUpdate()
        {
            physicsSystems.Foreach(system => system.FixedUpdate());
        }

        private void LateUpdate()
        {
            lateUpdateSystems.Foreach(system=> system.LateUpdate());
        }

        private void OnDestroy()
        {
            systems.Foreach(system=> system.Stop());
        }

        private void OnDrawGizmos()
        {
            GizmosEvent?.Invoke();
        }
    }
}