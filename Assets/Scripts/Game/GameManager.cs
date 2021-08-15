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

namespace Game
{
    public class GameManager : BaseComponent
    {
        public static event Action ReadSceneEvent;
        public static event Action StartEvent;
        public static event Action UpdateEvent;
        public static event Action FixedUpdateEvent;
        public static event Action LateUpdateEvent;
        public static event Action EndEvent;
        public static event Action GizmosEvent;
        public static GameProperties Properties => current.properties;
        public static GameManager current { get; set; }

        [SerializeField] private GameProperties properties;
 
        private static List<IGameSystem> systems = new List<IGameSystem>();
        private static GenericMap<IGameSystem> systemsMap = new GenericMap<IGameSystem>();

        private readonly DependencyContainer diContainer = new DependencyContainer();

        private static T AddSystem<T>(T system) where T : IGameSystem
        {
            systems.Add(system);
            systemsMap.Set(system);
            
            return system;
        }

        public ShipActor currentShip;

        private void Awake()
        {
            diContainer.Register(AddSystem(new WindSystem()));
            diContainer.Register(AddSystem(new AiCharacterSystem()));
            diContainer.Register(AddSystem(new GameCharacterSystem()));
            diContainer.Register(AddSystem(new ShipsSystem()));
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
            ReadSceneEvent?.Invoke();
            systems.Foreach(system=> system.Start());
            StartEvent?.Invoke();
        }


        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        private void LateUpdate()
        {
            LateUpdateEvent?.Invoke();
        }

        private void OnDestroy()
        {
            systems.Foreach(system=> system.Stop());
            EndEvent?.Invoke();
        }

        private void OnDrawGizmos()
        {
            GizmosEvent?.Invoke();
        }
    }
}