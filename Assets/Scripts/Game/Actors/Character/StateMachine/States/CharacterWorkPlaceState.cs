using Game.Actors.Character.Core.Motors;
using Game.Actors.Character.Interactions;
using UnityEngine;

namespace Game.Actors.Character.StateMachine.States
{
    public class CharacterWorkPlaceState : BaseCharacterState<RootMotionMotor>
    {
        private WorkPlace _place;
        public override RuntimeAnimatorController Animator => _place.animator;

        public void Setup(WorkPlace place)
        {
            _place = place;
        }


    }
}