using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Data;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    [Serializable]
    public class WorkPlaceMapUpdateAll : Action
    {
        [RequiredField] public SharedWorkPlaceMap map;
        public SharedWorkPlace exclude;
        public BehaviorFloatModifier modifier;

        public override TaskStatus OnUpdate()
        {
            foreach (var entry in map.Value)
            {
                if (Equals(entry.key, exclude.Value)) continue;
                entry.value = Mathf.Max(modifier.Apply(entry.value), 0);
            }

            return TaskStatus.Success;
        }
    }
}