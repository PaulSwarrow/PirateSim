using System;
using DI;
using Game.Actors.Character.Motors.Settings.Impl;
using Lib.Navigation;
using UnityEngine;
using static Game.Actors.Character.StateMachine.CharacterInputFlag;
using static Game.Actors.Character.StateMachine.CharacterInputTrigger;

namespace Game.Actors.Character.StateMachine.States
{
    public class CharacterActiveState : BaseCharacterState<NavmeshCharacterMotor>
    {
        // private static readonly int ForwardKey = Animator.StringToHash("Forward");
        // private static readonly int InAirKey = Animator.StringToHash("InAir");

        [SerializeField] private float walkSpeed = 2;
        [SerializeField] private float runSpeed = 4;

        [Inject] private DynamicNavmeshAgent _agent;


        private bool isTraveling;
        public override void Start()
        {
            base.Start();
            _agent.CheckSurface();
            // _agent.Forward; //bad code - bind to be adter checkSurface()
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

            if (_agent.IsTraveling)
            {
            }
            
        }

        public override RuntimeAnimatorController Animator => context.settings.animator;
    }
}