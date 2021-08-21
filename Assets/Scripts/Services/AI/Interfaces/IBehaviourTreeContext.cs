namespace Services.AI.Interfaces
{
    public interface IBehaviourTreeContext
    {
        T GetProperty<T>(string property);
    }
}