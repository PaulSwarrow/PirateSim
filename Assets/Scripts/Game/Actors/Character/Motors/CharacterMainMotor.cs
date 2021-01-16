using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Actors.Character.Motors
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
            Actor.navigator.CheckSurface();
            Actor.navigator.Forward = Forward = Actor.transform.forward; //bad code - bind to be adter checkSurface()
            Actor.view.MoveEvent += OnAnimatorMove;

            blendWeights = 0;
            tween?.Kill();
            tween = DOTween.To(() => blendWeights, v => blendWeights = v, 1, 5)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => tween = null);

            Actor.navigator.Sync(blendWeights);
        }

        public override void Update()
        {
            if (!active) return;
            Actor.navigator.Sync(blendWeights);
            Actor.view.animator.SetFloat(ForwardKey, NormalizedVelocity.z);
            Actor.navigator.Forward = Forward;
        }

        private void OnAnimatorMove()
        {
            Actor.navigator.Move(Actor.view.deltaPosition);
        }

        protected override void OnDisable()
        {
            Actor.view.MoveEvent -= OnAnimatorMove;
        }

        //API:
        public Vector3 NormalizedVelocity { get; set; }

        public Vector3 Forward { get; set; }
    }
}