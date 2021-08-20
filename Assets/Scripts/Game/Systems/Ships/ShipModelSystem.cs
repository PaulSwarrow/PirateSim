using DI;
using Game.Actors.Ship;
using Game.Models;
using Game.Systems.Abstract;
using Game.Systems.Characters;

namespace Game.Systems
{
    public class ShipModelSystem : ModelSystem<ShipModel>
    {
        [Inject] private LivingAreaSystem livingArea;
        [Inject] private ShipsCrewSystem crewSystem;

        public void CreateShip(ShipActor actor)
        {
            var ship = Create();
            ship.actor = actor;
            ship.livingArea = livingArea.CreateArea(actor.NavSurface);
            ship.crew = crewSystem.CreateCrew(10, ship.livingArea);
        }
    }
}