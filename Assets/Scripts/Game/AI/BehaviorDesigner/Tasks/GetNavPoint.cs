using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Lib.Navigation;

namespace Game.AI.BehaviorDesigner.Tasks
{
    public class GetNavPoint : Action
    {
        public SharedGameObject gameObject;
        public SharedNavPoint variableToStore;
        public float distance;

        public override TaskStatus OnUpdate()
        {
            if (DynamicNavmesh.SamplePosition(gameObject.Value.transform.position, out var point, distance))
            {
                variableToStore.Value = point;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            base.OnReset();
        }
    }
}