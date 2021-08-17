namespace Game.Actors.Character
{
    public class ModelActor<T> : IActor where T : ModelActor<T>
    {
        public ModelActor()
        {
            ActorTracker<T>.Register((T) this);
        }

        public void Dispose()
        {
            ActorTracker<T>.Unregister((T) this);
        }
        
    }
}