using DI;
using Game.Actors.Character.Core;
using UnityEngine;

namespace Game.Actors.Character.StateMachine.States
{
    public abstract class BaseCharacterState<TMotor> : ICharacterState
    where TMotor : ICharacterMotor, new()
    {
        [Inject] protected CharacterActorContext context;
        protected ICharacterInput Input => context.input;
        public virtual void Start()
        {
            context.core.SetMotor<TMotor>();
        }


        public virtual void Update()
        {
        }

        public virtual void End()
        {
        }

        
        public bool IsComplete => false;
        public bool Interruptable => true;
        public ICharacterState NextState { get; protected set; }
    }
}