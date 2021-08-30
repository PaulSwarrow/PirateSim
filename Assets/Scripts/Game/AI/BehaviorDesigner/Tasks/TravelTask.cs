using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;
using Lib.Navigation;

namespace Game.AI.BehaviorDesigner.Tasks
{
    public class TravelTask : BaseCharacterAction
    {
        [RequiredField]
        public SharedNavPoint point;
        public float accurancy = 0.1f;

        public override void OnStart()
        {
            base.OnStart();
            actor.Value.navigator.StartTravel(point.Value);
            
        }

        public override TaskStatus OnUpdate()
        {
            var reached = NavPoint.Distance(point.Value, actor.Value.navigator.GetCurrentNavPoint()) < accurancy;
            return reached ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            actor.Value.navigator.StopTravel();
            base.OnEnd();
        }

        public override void OnReset()
        {
            base.OnReset();
            point = null;
            actor = null;
        }
    }
}