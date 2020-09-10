using System;
using Lib;
using ShipSystems;
using UnityEngine;

namespace App
{
    public class GameManager : BaseComponent
    {
        public SailingConstantsConfig sailsConfig;
        public static GameManager current { get; set; }
        private GameSystem[] systems = 
        {
            new WindSystem(), 
        };

        public ShipEntity currentShip;
        private GenericMap<GameSystem> systemsMap = new GenericMap<GameSystem>();

        public T GetSystem<T>() where T : GameSystem => systemsMap.Get<T>();
        
        

        private void Awake()
        {
            current = this;
            foreach (var gameSystem in systems)
            {
                systemsMap.Set(gameSystem);
            }

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
        }

        private void OnDrawGizmos()
        {
            
            foreach (var gameSystem in systems)
            {
                gameSystem.DrawGizmos();
            }
        }
    }
}