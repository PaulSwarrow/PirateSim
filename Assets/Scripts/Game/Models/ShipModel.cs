using Game.Actors.Ship;
using Game.Interfaces;

namespace Game.Models
{
    public class ShipModel
    {
        public ShipActor actor;
        public ICharacterLiveArea livingArea;
        public Crew crew;
    }
}