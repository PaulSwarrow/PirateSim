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
     */
    [RequireComponent(typeof(DynamicNavmeshAgent))]
    public class GameCharacterAgent : BaseComponent
    {
        
        [SerializeField] public CharacterMainMotor defaultMotor;
        public GameCharacterView view { get; private set; }
        public DynamicNavmeshAgent navigator { get; private set; }

        private void Awake()
        {
            view = GetComponentInChildren<GameCharacterView>();
            navigator = GetComponent<DynamicNavmeshAgent>();
            GameCharacterSystem.AddAgent(this);
        }
    }
}