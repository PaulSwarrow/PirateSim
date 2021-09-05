using System.Collections;
using UnityEngine;

namespace Game.Actors.Character.StateMachine.Transitions
{
    public abstract class BaseCoroutineTransitionState : ICharacterState
    {
        public void Start()
        {
            IsComplete = false;
            GameManager.current.StartCoroutine(Coroutine());

        }

        public void Update()
        {
        }

        public void End()
        {
        }

        private IEnumerator Coroutine()
        {
            yield return TransitionCoroutine();
            IsComplete = true;
        }

        protected abstract IEnumerator TransitionCoroutine();

        public bool IsComplete { get; private set; }
        public bool Interruptable => false;
        public abstract ICharacterState NextState { get; }
    }
}