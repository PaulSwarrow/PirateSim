using System;
using System.Collections;
using System.Runtime.InteropServices;
using DI;
using Game.Actors.Character.Interactions;
using Game.Actors.Character.StateMachine.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Actors.Character.StateMachine.Transitions
{
    public class ExitWorkPlaceTransition : BaseCoroutineTransitionState
    {
        [Inject] private GameCharacterActor _actor;
        [Inject] private CharacterActiveState _targetState;
        private Action _callback;
        public override ICharacterState NextState => _targetState;

        public void Setup(Action callback)
        {
            _callback = callback;
        }
        
        protected override IEnumerator TransitionCoroutine()
        {
            Assert.IsNotNull(_actor.currentWorkPlace);
            
            yield return Cutscene.TransitionCutscene(
                _actor,
                _actor.currentWorkPlace.exitScene,
                _targetState.Animator);
            
            _actor.transform.SetParent(null, true);
            _actor.currentWorkPlace.Release();
            _actor.currentWorkPlace = null;
            
            _callback?.Invoke();
            
        }

    }
}