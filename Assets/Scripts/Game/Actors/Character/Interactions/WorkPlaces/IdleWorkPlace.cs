using System;
using Game.Actors.Character.Motors;
using UnityEngine;

namespace Game.Actors.Character.Interactions
{
    public class IdleWorkPlace : WorkPlace
    {
        [Serializable]
        public class  Motor: CharacterMotor
        {
            protected override void OnEnable()
            {
                
            }

            public override void Update()
            {
            }

            protected override void OnDisable()
            {
            }
        }


        [SerializeField] private WorkSpaceCharacterMotor motor;
        public override bool AllowChilling => true;
        public override CharacterMotor characterMotor => motor;
    }
}