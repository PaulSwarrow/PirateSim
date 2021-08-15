using System;
using App.SceneContext;
using DefaultNamespace;
using Lib;

namespace Game.Actors.Character
{
    public abstract class BaseActor : BaseComponent
    {
        
    }
    
    public abstract class BaseActor<T> : BaseActor where T: BaseActor<T>
    {
        private void OnEnable()
        {
            ActorTracker<T>.Register((T) this);
        }

        private void OnDisable()
        {
            ActorTracker<T>.Unregister((T) this);
            
        }
    }
}