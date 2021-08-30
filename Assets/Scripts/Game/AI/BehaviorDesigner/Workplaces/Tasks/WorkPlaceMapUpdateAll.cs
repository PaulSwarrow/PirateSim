using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Data;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    public class WorkPlaceMapUpdateAll : Action
    {
        [RequiredField] public SharedWorkPlaceMap map;
        public SharedWorkPlace exclude;
        public FloatModifiers modificator;

        public override TaskStatus OnUpdate()
        {
            foreach (var entry in map.Value)
            {
                if (Equals(entry.key, exclude.Value)) continue;
                entry.value = modificator.Apply(entry.value);
            }

            return TaskStatus.Success;
        }
    }
}