namespace Game.Actors.Character.Statemachine
{
    public class StateHandler
    {
        
    }
    public abstract class StateHandler<T>: StateHandler
    {
        public abstract void Handle(T state);
    }
    
}