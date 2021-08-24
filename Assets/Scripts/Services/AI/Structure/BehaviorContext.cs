using Services.AI.Interfaces;

namespace Services.AI.Structure
{
    
    public class BehaviorContext : IBehaviorContext
    {
        public IBehaviorDataProvider Target { get; set; }
        public IBehaviorDataProvider Current { get; set; }
    }
}