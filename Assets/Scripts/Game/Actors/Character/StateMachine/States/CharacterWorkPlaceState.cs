using Game.Actors.Character.Core.Motors;
using Game.Actors.Character.Interactions;
using UnityEngine;

namespace Game.Actors.Character.StateMachine.States
{
    public class CharacterWorkPlaceState : BaseCharacterState<RootMotionMotor>
    {
        private WorkPlace _place;

        public void Setup(WorkPlace place)
        {
            _place = place;
        }

        public override void Start()
        {
            base.Start();
            context.animator.runtimeAnimatorController = _place.animator;
        }

    }
}