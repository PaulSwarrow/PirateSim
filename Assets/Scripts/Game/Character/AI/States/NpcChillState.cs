using App.AI;

namespace App.Character.AI.States
{
    public class NpcChillState : BaseWorkingState<WorkPlace>
    {
        protected override WorkPlace workPlace { get; }
        protected override void OnEntered()
        {
            
        }

        protected override void OnWorking()
        {
        }
        

        protected override void OnExit()
        {
            stateMachine.RequireState<NpcBoredIdleState>();
        }
    }
}