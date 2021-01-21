using System;
using System.Collections.Generic;
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

        private static T AddSystem<T>(T system) where T : IGameSystem
        {
            systems.Add(system);
            systemsMap.Set(system);
            return system;
        }

        public static readonly WindSystem Wind = AddSystem(new WindSystem());
        public static readonly AiCharacterSystem Npc = AddSystem(new AiCharacterSystem());
        public static readonly GameCharacterSystem Characters = AddSystem(new GameCharacterSystem());
        public static readonly ShipsSystem Ships = AddSystem(new ShipsSystem());
        public static readonly UserControlSystem CharacterUserControl = AddSystem(new UserControlSystem());
        public static readonly UserCharacterHud CharacterHud = AddSystem(new UserCharacterHud());
        public static readonly LivingAreaSystem LivingAreaSystem = AddSystem(new LivingAreaSystem());
        public static readonly ShipsCrewSystem CrewSystem = AddSystem(new ShipsCrewSystem());
        public static readonly ObjsetSpawnSystem SpawnManager = AddSystem(new ObjsetSpawnSystem());

        public T GetSystem<T>() where T : IGameSystem => systemsMap.Get<T>();


        public ShipActor currentShip;

        private void Awake()
        {
            current = this;
            systems.Foreach(system=> system.Init());
        }

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

        public List<T> GetSystems<T>()
        {
            var result = new List<T>();
            foreach (var system in systems)
            {
                if (system is T tSystem) result.Add(tSystem);
            }

            return result;
        }
    }
}