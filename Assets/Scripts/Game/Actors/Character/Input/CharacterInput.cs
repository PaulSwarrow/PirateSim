using Game.Actors.Character.StateMachine;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character.Input
{
    public class CharacterInput :ICharacterInput
    {

        public void Move(Vector3 distance)
        {
            
        }

        public void Rotate(Vector3 vector)
        {
            
        }

        public void CleanUp()
        {
            _movement = Vector3.zero;
            _triggers = 0;
        }
        
        private int _flags;
        private int _triggers;
        private Vector3 _movement;
        private Vector3 _forward;
        private NavPoint _destination;

        public bool HasFlag(int mask) => (_flags & mask) != 0;
        public bool HasTrigger(int mask) => (_triggers & mask) != 0;

        public Vector3 Movement => _movement;//conflicts with target
        public Vector3 Forward => _forward;
        public NavPoint Destination
        {
            get => _destination;
            set => _destination = value;
        }

        public void Trigger(int mask)
        {
            _triggers = _triggers | mask;
        }
        public void Set(int mask, bool value)
        {
            if (value) _flags = _flags | mask;
            else _flags = _flags & ~mask;
        }
    }
}