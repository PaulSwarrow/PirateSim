using App.Character.UserControl;
using Lib.UnityQuickTools.Collections;

namespace App.Character
{
    public abstract class CharacterStateMachine
    { 
        private GameCharacterState currentState;
        private GameCharacter character;
        private GenericMap<GameCharacterState> states;

        public void Init(GameCharacter character)
        {
            this.character = character;
            GameManager.UpdateEvent += Update;
            states = PrepareStates();
            states.Values.Foreach(item=>item.Init(character, this));
        }

        protected abstract GenericMap<GameCharacterState> PrepareStates();


        public void Dispose()
        {
            GameManager.UpdateEvent -= Update;
            states.Clear();
            character = null;
        }
        
        
        public void RequireState<T>() where T : GameCharacterState
        {
            SetState(states.Get<T>());
        }
        
        public void RequireState<T, TData>(TData data) where T : GameCharacterState<TData>
        {
            var state = states.Get<T>();
            state.SetData(data); //update state while it is acting already?
            SetState(state);
        }
        

        private void SetState(GameCharacterState state)
        {
            if(state == currentState) return;
            currentState?.Stop();
            currentState = state;
            currentState.Start();
        }


        private void Update()
        {
            currentState.Update();
        }
        
    }
}