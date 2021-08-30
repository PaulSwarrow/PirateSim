using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    public class ActorCheckWorkPlace : Action
    {
        [RequiredField] public SharedCharacterActor actor;
        [RequiredField] public SharedWorkPlace placeToCompare;

        public override TaskStatus OnUpdate()
        {
            return actor.Value.currentWorkPlace == placeToCompare.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}