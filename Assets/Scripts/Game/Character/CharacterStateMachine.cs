using App.Character.AI;
using App.Character.UserControl;
using Game.Character.Statemachine;
using Lib.UnityQuickTools.Collections;

namespace App.Character
{
    public abstract class CharacterStateMachine
    {
        private GameCharacterState currentState;
        private GameCharacter character;
        private GenericMap<GameCharacterState> states;
        private CharacterStatemachineTask currentTask;

        public void Init(GameCharacter character)
        {
            this.character = character;
            GameManager.UpdateEvent += Update;
            states = PrepareStates();
            states.Values.Foreach(item => item.Init(character, this));
        }

        protected abstract GenericMap<GameCharacterState> PrepareStates();


        public void Dispose()
        {
            GameManager.UpdateEvent -= Update;
            states.Clear();
            character = null;
        }

        public void ApplyTask(CharacterStatemachineTask task)
        {
            if(currentTask == task) return;
            currentTask?.Stop();
            currentTask = task;
            currentTask.Start();
        }

        public void RequireState<T>() where T : GameCharacterState
        {
            SetState(states.Get<T>());
        }

        public void RequireState<T, TData>(TData data) where T : GameCharacterState, IStateWithData<TData>
        {
            var state = states.Get<T>();
            state.SetData(data);
            SetState(state);
        }


        private void SetState(GameCharacterState state)
        {
            if (state == currentState) return;
            currentState?.Stop();
            currentState = state;
            currentState.Start();
        }


        private void Update()
        {
            currentState.ReceiveTask(currentTask); //megre two calls?
            currentState.Update();
        }
    }
}