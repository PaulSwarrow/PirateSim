using UnityEngine;

namespace App.Character
{
    /*
     * Setup from agent or workplace
     * updates by character statemachine
     * converts custom input into motion and animation
     */
    public abstract class CharacterMotor
    {
        [SerializeField] private RuntimeAnimatorController animator;
        protected GameCharacterAgent agent { get; private set; }

        public void Enable(GameCharacterAgent agent)
        {
            this.agent = agent;
            if (animator)
                agent.view.animator.runtimeAnimatorController = animator;
            OnEnable();
        }

        public void Disable()
        {
            OnDisable();
            this.agent = null;
        }

        protected abstract void OnEnable();

        public abstract void Update();

        protected abstract void OnDisable();
    }
}