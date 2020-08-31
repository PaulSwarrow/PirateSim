using Lib;

namespace DefaultNamespace.Components
{
    public class StageScenarioPlayer : BaseComponent, IStageGoalProvider
    {
        public bool GoalAchieved { get; }
        public string GoalDescription { get; }
        public string GoalState { get; }
        
        
    }
}