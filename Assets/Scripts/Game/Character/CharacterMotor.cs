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
        protected GameCharacterAgent agent { get; private set; }

        public void Enable(GameCharacterAgent agent)
        {
            this.agent = agent;
            OnEnable();
        }

        public void Disable()
        {
            this.agent = null;
            OnDisable();
        }

        protected abstract void OnEnable();
        
        public abstract void Update();

        protected abstract void OnDisable();
    }
}