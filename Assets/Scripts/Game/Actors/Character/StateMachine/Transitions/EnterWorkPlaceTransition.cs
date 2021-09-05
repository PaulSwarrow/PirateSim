using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using DI;
using Game.Actors.Character.Interactions;
using Game.Actors.Character.StateMachine.States;
using Game.Actors.Workplaces;
using UnityEngine;
using UnityEngine.Assertions;
using Action = System.Action;

namespace Game.Actors.Character.StateMachine.Transitions
{
    public class EnterWorkPlaceTransition : BaseCoroutineTransitionState
    {
        [Inject] private GameCharacterActor _actor;
        [Inject] private CharacterWorkPlaceState _targetState;
        [Inject] private CharacterActorSettings _settings;
        private WorkPlace _place;
        private Action _callback;

        public override ICharacterState NextState => _targetState;

        public void Setup(WorkPlace place, Action callback)
        {
            _place = place;
            _callback = callback;
            _targetState.Setup(place);
        }
        
        protected override IEnumerator TransitionCoroutine()
        {
            Assert.IsNull(_actor.currentWorkPlace);
            _actor.currentWorkPlace = _place;
            yield return Cutscene.TransitionCutscene(_actor, _place.entryScene, _place.animator);
            _actor.transform.SetParent(_place.transform, true);
            _callback.Invoke();
        }
    }
}