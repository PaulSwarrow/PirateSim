using System;
using App.SceneContext;
using DefaultNamespace;
using Lib;

namespace Game.Actors.Character
{
    
    public abstract class SceneActor<T> : BaseComponent, IActor where T: SceneActor<T>
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