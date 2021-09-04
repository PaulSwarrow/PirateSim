using System;
using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.Systems;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    public class ActorEnterWorkPlace : BaseCharacterAction
    {
        [RequiredField] public SharedWorkPlace workPlace;
        private bool isComplete;
        
        public override void OnStart()
        {
            base.OnStart();
            isComplete = false;
            var character = actor.Value;
            if(character.currentWorkPlace != null)
            {
                throw  new Exception("Already in some work place");
            }
            
            if (character.currentWorkPlace == workPlace.Value) isComplete = true; //TODO weak code. can possibly be in a transition!
            else
            {
                actor.Value.StateMachine.EnterWorkPlace(workPlace.Value, OnComplete);
            }
        }

        private void OnComplete()
        {
            isComplete = true;
        }


        public override TaskStatus OnUpdate()
        {
            return isComplete ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnReset()
        {
            base.OnReset();
            isComplete = false;
        }
    }
}