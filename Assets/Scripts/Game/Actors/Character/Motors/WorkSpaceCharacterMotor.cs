using System;
using UnityEngine;

namespace Game.Actors.Character.Motors
{
    /*[Serializable]
    public class WorkSpaceCharacterMotor : CharacterMotor
    {
        private Vector3 localPosition;
        private Quaternion localRotation;
        protected override void OnEnable()
        {
            localPosition = Actor.transform.localPosition;
            localRotation = Actor.transform.localRotation;
            Actor.view.MoveEvent += OnAnimatorMove;
        }

        public override void Update()
        {
            
        }

        protected override void OnDisable()
        {
            Actor.view.MoveEvent -= OnAnimatorMove;
        }

        private void LateUpdate()
        {
            Actor.transform.localPosition = localPosition;
            Actor.transform.localRotation = localRotation;

        }

        private void OnAnimatorMove()
        {
            localPosition += Actor.view.deltaPosition;
            localRotation *= Actor.view.deltaRotation;
        }
    }*/
}