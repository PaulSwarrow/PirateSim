using System;
using DG.Tweening;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character.Motors
{
    [Serializable]
    public class CharacterMainMotor : CharacterMotor
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        private float blendWeights = 0;

        [SerializeField] private float walkSpeed = 2;
        [SerializeField] private float runSpeed = 4;


        private Tween tween;
        public bool active = true;
        private bool run;
        public Vector3 LookDirection => Actor.navigator.Forward;
        public Vector3 WorldVelocity => Actor.navigator.WorldVelocity;

        protected override void OnEnable()
        {
            Run = false;
            Actor.navigator.CheckSurface();
            Actor.navigator.Forward = Actor.transform.forward; //bad code - bind to be adter checkSurface()
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
            
            Actor.view.animator.SetFloat(ForwardKey, Actor.navigator.LocalVelocity.z);
        }


        private void OnAnimatorMove()
        {
            // Actor.navigator.Move(Actor.view.deltaPosition);
        }

        protected override void OnDisable()
        {
            Actor.view.MoveEvent -= OnAnimatorMove;
        }

        //API:
        public void Move(Vector3 direction)
        {
            Stop();
            direction = Vector3.ClampMagnitude(direction, 1);
            
            Actor.navigator.Move(direction * ((run ? runSpeed : walkSpeed)));
        }

        public bool Run
        {
            get => run;
            set
            {
                run = value;
                Actor.navigator.SetSpeed(value ? runSpeed : walkSpeed);
            }
        }

        public void Travel(NavPoint point)
        {
            Actor.navigator.StartTravel(point);
        }

        public void Look(Vector3 forward)
        {
            var current = Actor.navigator.Forward;
            var deltaQuaternion = Quaternion.FromToRotation(current, forward);
            deltaQuaternion = Quaternion.Lerp(Quaternion.identity, deltaQuaternion, 6.2f * Time.deltaTime);
            Actor.navigator.Forward = deltaQuaternion * current;
        }
        
        

        public void Stop()
        {
            Actor.navigator.StopTravel();
        }
    }
}