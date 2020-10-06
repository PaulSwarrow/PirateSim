using System;
using Lib;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace App.Character.Locomotion
{
    public class CharacterAnimator : BaseComponent
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        
       [SerializeField] private Animator animator;
       private ICharacterMotor motor;

       private void Awake()
       {
           motor = GetComponent<ICharacterMotor>();
       }

       private void Update()
        {
            animator.SetFloat(ForwardKey, motor.Velocity.magnitude);
            
        }
    }
}