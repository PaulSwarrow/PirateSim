using System.Collections.Generic;
using App.SceneContext;
using DI;
using Game.Actors.Ship;
using Game.Interfaces;
using Game.Models;
using Game.Systems.Characters;
using Game.Systems.Sea;
using Lib.UnityQuickTools.Collections;

namespace Game.Systems
{
    public class ShipsSystem : IGameSystem
    {
        [Inject] private LivingAreaSystem livingArea;
        [Inject] private ShipsCrewSystem crewSystem;
        [Inject] private WindSystem windSystem;

        private HashSet<ShipModel> items = new HashSet<ShipModel>();
        public void Init()
        {
            
        }
        
        public void Start()
        {
            ActorTracker<ShipActor>.All.Foreach(ShipFromActor);
            GameManager.UpdateEvent += OnUpdate;
        }

        public void Stop()
        {
            
        }

        private void OnUpdate()
        {
            foreach (var item in items)
            {
                var localWind = item.actor.transform.InverseTransformVector(windSystem.Force);
                item.actor.LocalWind = localWind;
            }
        }
        
        private void ShipFromActor(ShipActor actor)
        {
            var ship = new ShipModel
            {
                actor = actor,
                livingArea = livingArea.CreateArea(actor.NavSurface)
            };
            ship.crew = crewSystem.CreateCrew(10, ship.livingArea);
            items.Add(ship);
        }
    }
}