using System.Collections.Generic;
using App;

namespace ShipSystems
{
    public class ShipsSystem : GameSystem
    {
        private List<ShipEntity> ships = new List<ShipEntity>();
        
        public override void Update()
        {
            base.Update();
            foreach (var shipEntity in ships)
            {
                shipEntity.ApplyWind(GameManager.current.GetSystem<WindSystem>().Wind);
            }
        }
    }
}