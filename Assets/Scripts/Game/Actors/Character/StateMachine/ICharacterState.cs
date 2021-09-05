using UnityEngine;

namespace Game.Actors.Character.StateMachine
{
    public interface ICharacterState
    {
        void Start();
        void Update();
        void End();
        bool IsComplete { get; }
        bool Interruptable  { get; }
        ICharacterState NextState { get; }
    }
}