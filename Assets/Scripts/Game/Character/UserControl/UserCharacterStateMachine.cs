using System;
using System.Collections.Generic;
using App.AI;
using App.Character.UserControl;
using App.Tools;
using Lib.UnityQuickTools.Collections;

namespace App.Character
{
    public class UserCharacterStateMachine
    {
        private GameCharacterState currentState;
        private GameCharacter character;
        private GenericMap<GameCharacterState> states = new GenericMap<GameCharacterState>();

        public void Init(GameCharacter character)
        {
            this.character = character;
            GameManager.UpdateEvent += Update;
            states.Set(new CharacterMainInput());
            states.Set(new WorkingCharacterState());
            
            states.Values.Foreach(item=>item.Init(character));
            
            RequireState<CharacterMainInput>();
        }


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
            state.SetData(data);
            SetState(state);
        }
        

        private void SetState(GameCharacterState state)
        {
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