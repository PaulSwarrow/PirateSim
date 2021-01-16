using System;
using System.Collections.Generic;
using App.Character;
using App.Character.AI;
using App.Character.UserControl;
using App.Character.UserControl.Modules;
using Lib;
using ShipSystems;

namespace App
{
    public class GameManager : BaseComponent
    {
        public static event Action UpdateEvent;
        public static event Action FixedUpdateEvent;
        public static event Action LateUpdateEvent;
        public SailingConstantsConfig sailsConfig;
        public static GameManager current { get; set; }
        private static List<GameSystem> systems = new List<GameSystem>();
        private static GenericMap<GameSystem> systemsMap = new GenericMap<GameSystem>();

        private static T AddSystem<T>(T system) where T : GameSystem
        {
            systems.Add(system);
            systemsMap.Set(system);
            return system;
        }

        public static readonly WindSystem Wind = AddSystem(new WindSystem());
        public static readonly GameCharacterSystem Characters = AddSystem(new GameCharacterSystem());
        public static readonly UserControlSystem CharacterUserControl = AddSystem(new UserControlSystem());
        public static readonly UserCharacterHud CharacterHud = AddSystem(new UserCharacterHud());
        public static readonly AiCharacterSystem Npc = AddSystem(new AiCharacterSystem());

        public T GetSystem<T>() where T : GameSystem => systemsMap.Get<T>();


        public ShipEntity currentShip;

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            foreach (var gameSystem in systems)
            {
                gameSystem.Start();
            }
        }


        private void Update()
        {
            foreach (var gameSystem in systems)
            {
                gameSystem.Update();
            }
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