using System;
using DI;
using Game.Actors.Character.Interactions;
using Game.Actors.Character.StateMachine.States;
using Game.Actors.Character.StateMachine.Transitions;
using Game.Actors.Workplaces;
using Lib.Navigation;
using UnityEngine.Assertions;

namespace Game.Actors.Character.StateMachine
{
    public class CharacterStateMachine
    {
        private GenericMap<ICharacterState> states = new GenericMap<ICharacterState>();
        private DependencyContainer di;

        private ICharacterState current;

        public CharacterStateMachine(DependencyContainer actorContext)
        {
            di = new DependencyContainer(actorContext);

            AddState<CharacterWorkPlaceState>();
            AddState<CharacterActiveState>();
            AddState<EnterWorkPlaceTransition>();
            AddState<ExitWorkPlaceTransition>();
            
            di.InjectDependencies();
            
            current = states.Get<CharacterActiveState>();
            current.Start();
        }

        //API:
        public void ExitWorkPlace(Action callback)
        {
            var transition = states.Get<ExitWorkPlaceTransition>();
            transition.Setup(callback);
            SetState(transition);
            
        }
        
        public void EnterWorkPlace(WorkPlace place, Action callback)
        {
            var transition = states.Get<EnterWorkPlaceTransition>();
            transition.Setup(place, callback);
            SetState(transition);
        }

        
        //internal lifecycle
        private void AddState<T>() where T : class, ICharacterState, new()
        {
            var state = new T();
            states.Set(state);
            di.Register(state);
        }

        private void SetState(ICharacterState state)
        {
            Assert.IsTrue(current.IsComplete || current.Interruptable, $"unable to transit from {current} to {state}");
            current?.End();
            current = state;
            current.Start();
        }


        public void Update()
        {
            current.Update();
            if (current.IsComplete && current.NextState != null)
            {
                SetState(current.NextState);
            }
            
        }
    }
}