using System;
using Game.Actors.Character;
using Game.Actors.Ship;
using Game.Interfaces;

namespace Game.Models
{
    public class ShipModel  : IDisposable
    {
        public ShipActor actor;
        public ICharacterLiveArea livingArea;
        public Crew crew;

        public void Dispose()
        {
            actor = null;
            livingArea = null;
            crew = null;
        }
    }
}