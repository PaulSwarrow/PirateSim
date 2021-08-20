using System.Collections.Generic;
using App.SceneContext;
using DI;
using Game.Actors;
using Game.Actors.Ship;
using Game.Interfaces;
using Game.Models;
using Game.Systems.Abstract;
using Game.Systems.Characters;
using Game.Systems.Sea;
using Lib.UnityQuickTools.Collections;

namespace Game.Systems
{
    public class ShipActorsSystem : ActorSystem<ShipActor>
    {
        [Inject] private WindSystem windSystem;
        [Inject] private ShipModelSystem modelSystem;
        protected override void OnStart(ShipActor actor)
        {
            base.OnStart(actor);
            modelSystem.CreateShip(actor);
        }

        protected override void OnUpdate(ShipActor actor)
        {
            base.OnUpdate(actor);
            actor.LocalWind = actor.transform.InverseTransformVector(windSystem.Force);
        }
    }
}