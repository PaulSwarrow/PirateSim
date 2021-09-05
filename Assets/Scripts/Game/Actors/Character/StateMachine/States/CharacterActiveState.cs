using System;
using DI;
using Game.Actors.Character.Core.Motors;
using Lib.Navigation;
using UnityEngine;
using static Game.Actors.Character.Input.CharacterInputFlag;
using static Game.Actors.Character.Input.CharacterInputTrigger;

namespace Game.Actors.Character.StateMachine.States
{
    public class CharacterActiveState : BaseCharacterState<NavmeshCharacterMotor>
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        [Inject] private Animator _animator;

        [Inject] private DynamicNavmeshAgent _agent;


        private bool isTraveling;

        public override void Start()
        {
            base.Start();
            _agent.CheckSurface();
            context.animator.runtimeAnimatorController = context.settings.animator;
            // _agent.Forward; //bad code - bind to be adter checkSurface()
        }

        public override void End()
        {
            base.End();
            if (_agent.IsTraveling) _agent.StopTravel();
         }

        public override void Update()
        {
            base.Update();
            var move = Input.Movement;

            if (Input.HasTrigger(StartTravel))
            {
                _agent.StartTravel(Input.Destination);
            }
            else
            {
                if (move.magnitude > 0.01f)
                {
                    if(_agent.IsTraveling) _agent.StopTravel();
                    move *= context.settings.walkSpeed;
                    _agent.Move(move);
                }
            }
            
            _animator.SetFloat(ForwardKey, _agent.RelactiveVelocity.z);
            

            if (_agent.IsTraveling)
            {
            }
            
        }
    }
}