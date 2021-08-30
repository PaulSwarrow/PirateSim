using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Lib.UnityQuickTools.Collections;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    public class FindWorkPlace : BaseCharacterAction
    {
        [RequiredField] public SharedWorkPlaceMap placesAvailable;
        public float criticalValue = 1;
        [RequiredField] public SharedWorkPlace currentWorkPlace;
        public override TaskStatus OnUpdate()
        {
            if (placesAvailable.Value == null || placesAvailable.Value.Count == 0) return TaskStatus.Failure;
            if (currentWorkPlace.Value == null
                || !(placesAvailable.TryGetValue(currentWorkPlace.Value, out var value) && value < criticalValue))
            {
                var place = placesAvailable.Value.Least(entry => entry.value);
                currentWorkPlace.Value = place.key;
            }

            return TaskStatus.Success;
        }
    }
}