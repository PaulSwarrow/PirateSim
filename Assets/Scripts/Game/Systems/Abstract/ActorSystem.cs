using Game.Actors;
using Game.Actors.Character;
using Game.Interfaces;
using Lib.UnityQuickTools.Collections;

namespace Game.Systems.Abstract
{
    public abstract class ActorSystem<T>: IGameSystem, IGameUpdateSystem, IGamePhysicsSystem 
    where T: IActor
    {
        public virtual void Init()
        {
            
        }

        public void Start()
        {
            ActorTracker<T>.All.Foreach(OnStart);
        }

        public void Stop()
        {
        }

        public void Update()
        {
            ActorTracker<T>.All.Foreach(OnUpdate);
        }

        public void FixedUpdate()
        {
            ActorTracker<T>.All.Foreach(OnFixedUpdate);
        }

        protected virtual void OnStart(T actor)
        {
            
        }

        protected virtual void OnUpdate(T actor)
        {
            
        }
        protected virtual void OnFixedUpdate(T actor)
        {
            
        }
    }
}