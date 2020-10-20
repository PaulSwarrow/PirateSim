using System;
using App.Navigation;
using UnityEngine;

namespace App.Character
{
    [Serializable]
    public class CharacterMainMotor : CharacterMotor
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");

        
        
        protected override void OnEnable()
        {
            Forward = agent.navigator.Forward = agent.transform.forward;
            agent.navigator.CheckSurface();
            agent.view.MoveEvent += OnAnimatorMove;
            agent.navigator.Sync();

        }

        public override void Update()
        {
            agent.navigator.Sync();
            agent.view.animator.SetFloat(ForwardKey, NormalizedVelocity.z);
            agent.navigator.Forward = Forward;
        }

        private void OnAnimatorMove()
        {
            agent.navigator.Move(agent.view.deltaPosition);
        }

        protected override void OnDisable()
        {
            agent.view.MoveEvent -= OnAnimatorMove;
            
        }

        //API:
        public Vector3 NormalizedVelocity { get; set; }

        public Vector3 Forward { get; set; }
    }
}