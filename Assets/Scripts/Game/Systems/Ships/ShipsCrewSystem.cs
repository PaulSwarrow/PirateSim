using Game.Interfaces;
using Game.Models;

namespace Game.Systems
{
    public class ShipsCrewSystem : IGameSystem
    {
        public void Init()
        {
            
        }

        public Crew CreateCrew(ICharacterLiveArea livingArea)
        {
            return new Crew
            {
                livigArea = livingArea
            };
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}