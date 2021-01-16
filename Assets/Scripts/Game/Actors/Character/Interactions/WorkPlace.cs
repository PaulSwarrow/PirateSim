using System;
using Lib;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace Game.Actors.Character.Interactions
{
    public abstract class WorkPlace : BaseComponent
    {
        public event Action TakenEvent; 
        public event Action ReleasedEvent;
        
        [SerializeField] public PlayableDirector entryScene;
        [SerializeField] public PlayableDirector exitScene;
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