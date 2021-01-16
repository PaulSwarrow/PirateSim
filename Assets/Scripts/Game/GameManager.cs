using System;
using System.Collections.Generic;
using Game.Actors.Ship;
using Game.Interfaces;
using Game.Systems.Characters;
using Game.Systems.Sea;
using Lib;
using Lib.UnityQuickTools.Collections;

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
        public SailingConstantsConfig sailsConfig;
        public static GameManager current { get; set; }
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
        public static readonly UserControlSystem CharacterUserControl = AddSystem(new UserControlSystem());
        public static readonly UserCharacterHud CharacterHud = AddSystem(new UserCharacterHud());

        public T GetSystem<T>() where T : IGameSystem => systemsMap.Get<T>();


        public ShipEntity currentShip;

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
            EndEvent?.Invoke();
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