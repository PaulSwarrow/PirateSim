using System;
using UnityEngine;

namespace App.Character
{
    [Serializable]
    public class WorkSpaceCharacterMotor : CharacterMotor
    {
        private Vector3 localPosition;
        private Quaternion localRotation;
        protected override void OnEnable()
        {
            localPosition = agent.transform.localPosition;
            localRotation = agent.transform.localRotation;
            GameManager.LateUpdateEvent += OnLateUpdate;
            agent.view.MoveEvent += OnAnimatorMove;
        }

        public override void Update()
        {
            
        }

        protected override void OnDisable()
        {
            GameManager.LateUpdateEvent -= OnLateUpdate;
            agent.view.MoveEvent -= OnAnimatorMove;
        }

        private void OnLateUpdate()
        {
            agent.transform.localPosition = localPosition;
            agent.transform.localRotation = localRotation;

        }

        private void OnAnimatorMove()
        {
            localPosition += agent.view.deltaPosition;
            localRotation *= agent.view.deltaRotation;
        }
    }
}