namespace DefaultNamespace
{
    public interface IStageGoalProvider
    {
        bool GoalAchieved { get; }
        string GoalDescription { get; }
        string GoalState { get; }
    }
}