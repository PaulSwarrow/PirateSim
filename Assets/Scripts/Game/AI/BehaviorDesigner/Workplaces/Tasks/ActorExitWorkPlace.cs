using BehaviorDesigner.Runtime.Tasks;
using Game.Actors.Character.Interactions;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.Systems;
using HutongGames.PlayMaker;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    public class ActorExitWorkPlace : BaseCharacterAction
    {
        private bool isComplete;
        
        public override void OnStart()
        {
            base.OnStart();
            var character = actor.Value;
            isComplete = false;
            if (character.currentWorkPlace == null) isComplete = true; //TODO weak code. can possibly be in a transition!
            else
            {
                actor.Value.StateMachine.ExitWorkPlace(OnComplete);
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