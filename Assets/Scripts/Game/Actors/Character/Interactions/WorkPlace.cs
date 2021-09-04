using System;
using Lib;
using Lib.Navigation;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace Game.Actors.Character.Interactions
{
    public abstract class WorkPlace : SceneActor<WorkPlace>
    {
        public abstract bool AllowChilling { get; }
        
        public event Action TakenEvent; 
        public event Action ReleasedEvent;
        
        [SerializeField] public PlayableDirector entryScene;
        [SerializeField] public PlayableDirector exitScene;
        [SerializeField] public RuntimeAnimatorController animator;
        private GameCharacterActor character;

        //TODO bake in surface owner for this workplace for optimization and safety
        public NavPoint EnterPosition => DynamicNavmesh.RequirePosition(entryScene.transform.position);

        private void Awake()
        {
        }

        public GameCharacterActor Visitor => character;

        public bool Occupied { get; private set; }

        public void Occupy(GameCharacterActor character)
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