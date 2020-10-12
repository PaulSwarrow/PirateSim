using System;
using System.Collections.Generic;
using App.Navigation;
using Lib;
using UnityEngine;

namespace App.Character
{
    /*
     * A concrete instance of the character.
     * Provides facade api for GameCharacterMotdel
     * Can create character models on scene awake
     * Exists only in loaded/rendered area
     */
    [RequireComponent(typeof(DynamicNavmeshAgent))]
    public class GameCharacterAgent : BaseComponent
    {
        
        [SerializeField] public CharacterMainMotor defaultMotor;
        private CharacterMotor motor;
        public GameCharacterView view { get; private set; }
        public DynamicNavmeshAgent navigator { get; private set; }

        private void Awake()
        {
            view = GetComponentInChildren<GameCharacterView>();
            navigator = GetComponent<DynamicNavmeshAgent>();
            GameCharacterSystem.AddAgent(this);
        }
        
        
        public void SetMotor(CharacterMotor motor)
        {
            if(this.motor == motor) return;
            if(this.motor != null) motor.Disable();
            motor.Enable(this);
            this.motor = motor;
        }

        private void Update()
        {
            motor.Update();
        }
    }
}