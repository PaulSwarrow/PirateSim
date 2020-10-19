using System;
using App.Character;
using UnityEngine;

namespace App.AI
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
        public override CharacterMotor characterMotor => motor;
    }
}