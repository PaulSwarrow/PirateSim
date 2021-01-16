using System;
using UnityEditorInternal;

namespace App.Character.Statemachine
{
    public class StateHandler
    {
        
    }
    public abstract class StateHandler<T>: StateHandler
    {
        public abstract void Handle(T state);
    }
    
}