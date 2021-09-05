using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    [Serializable]
    public class WorkPlaceMapCooldown : Action
    {
        [RequiredField] public SharedWorkPlaceMap map;
        public float modifier = 1;

        public override TaskStatus OnUpdate()
        {
            var delta = Time.deltaTime * modifier;
            foreach (var entry in map.Value)
            {
                entry.value = Mathf.Max(entry.value - delta, 0);
            }

            return TaskStatus.Success;
        }
    }
}