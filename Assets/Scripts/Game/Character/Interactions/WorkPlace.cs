using System;
using System.Collections;
using App.Character;
using Lib;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace App.AI
{
    public abstract class WorkPlace : BaseComponent
    {
        public event Action TakenEvent; 
        public event Action ReleasedEvent;
        
        [SerializeField] public PlayableDirector entryScene;
        private GameCharacter character;

        private void Awake()
        {
        }


        public bool Occupied { get; private set; }
        public abstract CharacterMotor characterMotor { get;}

        public void Occupy(GameCharacter character)
        {
            Assert.IsNull(this.character);
            this.character = character;
        }

        public void Release()
        {
            var view = character;
            character = null;
            ReleasedEvent?.Invoke();
        }
    }
}