using System.Collections.Generic;
using App.SceneContext;
using DI;
using Game.Actors;
using Game.Actors.Ship;
using Game.Interfaces;
using Game.Models;
using Game.Systems.Characters;
using Game.Systems.Sea;
using Lib.UnityQuickTools.Collections;

namespace Game.Systems
{
    public class ShipsSystem : IGameSystem, IGameUpdateSystem
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
        }

        public void Update()
        {
            foreach (var item in items)
            {
                var localWind = item.actor.transform.InverseTransformVector(windSystem.Force);
                item.actor.LocalWind = localWind;
            }
        }

        public void Stop()
        {
            
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