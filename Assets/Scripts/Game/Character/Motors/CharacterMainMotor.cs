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

        [SerializeField] private RuntimeAnimatorController animator;
        
        
        protected override void OnEnable()
        {
            agent.view.animator.runtimeAnimatorController = animator;
            agent.navigator.CheckSurface();
            Forward = agent.navigator.Forward;
            agent.view.MoveEvent += OnAnimatorMove;

        }

        public override void Update()
        {
            agent.view.animator.SetFloat(ForwardKey, NormalizedVelocity.z);
            agent.navigator.Forward = Forward;
            agent.navigator.Sync();
        }

        private void OnAnimatorMove()
        {
            agent.transform.position += agent.view.deltaPosition;
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