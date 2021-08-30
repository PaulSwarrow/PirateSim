using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    public class GetCurrentWorkPlace : BaseGetVariablePropertyValue<SharedCharacterActor, SharedWorkPlace>
    {
        protected override object GetValue(SharedCharacterActor target)
        {
            return target.Value.currentWorkPlace;
        }
    }
}