using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character.StateMachine
{
    public interface ICharacterInput
    {
        Vector3 Movement { get; }
        Vector3 Forward { get; }
        
        NavPoint Destination { get; }
        bool HasFlag(int mask);
        bool HasTrigger(int mask);
    }
}