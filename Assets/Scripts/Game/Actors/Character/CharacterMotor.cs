using UnityEngine;

namespace Game.Actors.Character
{
    /*
     * Setup from agent or workplace
     * updates by character statemachine
     * converts custom input into motion and animation
     */
    public abstract class CharacterMotor
    {
        [SerializeField] public RuntimeAnimatorController animator;
        protected GameCharacterActor Actor { get; private set; }

        public void Enable(GameCharacterActor actor)
        {
            this.Actor = actor;
            if (animator && animator != actor.view.animator.runtimeAnimatorController)
                actor.view.animator.runtimeAnimatorController = animator;
            OnEnable();
        }

        public void Disable()
        {
            OnDisable();
            this.Actor = null;
        }

        protected abstract void OnEnable();

        public abstract void Update();

        protected abstract void OnDisable();
    }
}