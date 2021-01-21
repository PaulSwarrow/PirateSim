using System.Collections.Generic;
using Game.Interfaces;
using Game.Models;
using Lib.Navigation;

namespace Game.Systems.Characters
{
    public class LivingAreaSystem : IGameSystem
    {
        private List<ICharacterLiveArea> areas = new List<ICharacterLiveArea>();

        public void Init()
        {
        }

        public ICharacterLiveArea CreateArea(DynamicNavMeshSurface surface)
        {
            var area = new DynamicSurfaceLivingArea(surface);
            areas.Add(area);
            return area;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}