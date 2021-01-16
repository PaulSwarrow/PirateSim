using System.Collections;
using System.Collections.Generic;
using App.Character.Statemachine;
using Game.Character.Statemachine;

namespace App.Character.AI
{
    
    public abstract class CharacterStatemachineTask
    {
        private List<StateHandler> handlers;
        private StateHandler<BaseStateApi> defaultHandler;

        public CharacterStatemachineTask()
        {
            PrepareHandlers(out handlers, out defaultHandler);
        }


        protected abstract void PrepareHandlers(
            out List<StateHandler> handlers,
            out StateHandler<BaseStateApi> defaultHandler
        );


        public void Update<T>(T state) where T : BaseStateApi
        {
            //looks slow for every frame!
            foreach (var handler in handlers)
            {
                if (handler is StateHandler<T> correctHandler)
                {
                    correctHandler.Handle(state);
                    return;
                }
            }
            defaultHandler.Handle(state);
        }

        
        public virtual void Start()
        {
        }
        
        public virtual void Stop()
        {
        }
    }
}