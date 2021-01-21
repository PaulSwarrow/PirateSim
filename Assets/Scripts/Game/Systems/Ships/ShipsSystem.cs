using Game.Actors.Ship;
using Game.Interfaces;
using Game.Models;

namespace Game.Systems
{
    public class ShipsSystem : IGameSystem
    {
        public void Init()
        {
            
        }

        public void RegisterShip(ShipActor actor)
        {
            var ship = new ShipModel
            {
                actor = actor,
                livingArea = GameManager.LivingAreaSystem.CreateArea(actor.NavSurface)
            };
            ship.crew = GameManager.CrewSystem.CreateCrew(ship.livingArea);
        }
        
        public void Start()
        {
        }

        public void Stop()
        {
            
        }
    }
}