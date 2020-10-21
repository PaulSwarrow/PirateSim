using System;
using App.Navigation;
using DG.Tweening;
using UnityEngine;

namespace App.Character
{
    [Serializable]
    public class CharacterMainMotor : CharacterMotor
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        private float blendWeights = 0;


        private Tween tween;
        public bool active = true;

        protected override void OnEnable()
        {
            agent.navigator.CheckSurface();
            agent.navigator.Forward = Forward = agent.transform.forward; //bad code - bind to be adter checkSurface()
            agent.view.MoveEvent += OnAnimatorMove;

            blendWeights = 0;
            tween?.Kill();
            tween = DOTween.To(() => blendWeights, v => blendWeights = v, 1, 5)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => tween = null);

            agent.navigator.Sync(blendWeights);
        }

        public override void Update()
        {
            if (!active) return;
            agent.navigator.Sync(blendWeights);
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