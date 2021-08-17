namespace Game.Interfaces
{
    public interface IGameSystem
    {
        void Init();
        void Start();
        void Stop();
    }
    
    public interface IGameUpdateSystem
    {
        void Update();
    }

    public interface IGameLateUpdateSystem
    {
        void LateUpdate();
    }
    
    public interface IGamePhysicsSystem
    {
        void FixedUpdate();
    }
}