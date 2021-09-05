using System;
using System.Collections.Generic;
using Game.Actors.Character;
using Game.AI.BehaviorDesigner.Variables;
using Lib.Navigation;
using Lib.UnityQuickTools.Enums;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

namespace Game.Actors.Workplaces
{
    public abstract class WorkPlace : SceneActor<WorkPlace>
    {
        protected abstract WorkPlaceTag Tags { get; }
        
        public event Action OccupiedEvent; 
        public event Action ReleasedEvent;
        
        [SerializeField] public PlayableDirector entryScene;
        [SerializeField] public PlayableDirector exitScene;
        [SerializeField] public RuntimeAnimatorController animator;
        private GameCharacterActor _character;
        private Dictionary<WorkPlaceParameter, Func<float>> _parameters = new Dictionary<WorkPlaceParameter, Func<float>>();

        //TODO bake in surface owner for this workplace for optimization and safety
        public NavPoint EnterPosition => DynamicNavmesh.RequirePosition(entryScene.transform.position);

        protected virtual void Awake()
        {
        }

        public GameCharacterActor Visitor => _character;

        public bool Occupied { get; private set; }

        public void Occupy(GameCharacterActor character)
        {
            Assert.IsNull(this._character);
            this._character = character;
            OccupiedEvent?.Invoke();
        }

        protected void MapParameter(WorkPlaceParameter key, Func<float> getter)
        {
            _parameters.Add(key, getter);
        }

        public float GetParameter(WorkPlaceParameter key)
        {
            return _parameters[key].Invoke();
        }

        public void Release()
        {
            var view = _character;
            _character = null;
            ReleasedEvent?.Invoke();
        }

        public bool Check(WorkPlaceTag filter, EnumComparison filterMode, GameCharacterActor target= null)
        {
            var tags = Tags;
            if (!Occupied || Visitor == target) tags |= WorkPlaceTag.Empty;
            return tags.Compare(filter, filterMode);

        }
    }
}