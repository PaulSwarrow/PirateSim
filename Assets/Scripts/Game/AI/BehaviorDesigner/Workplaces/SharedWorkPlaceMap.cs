using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Game.Actors.Character.Interactions;
using Game.Actors.Workplaces;
using Game.AI.BehaviorDesigner.Data;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.AI.BehaviorDesigner.Workplaces
{
    [Serializable]
    public class SharedWorkPlaceMap : SharedDictionary<WorkPlace, float>
    {

        public static implicit operator SharedWorkPlaceMap(List<Entry> value)
        {
            return new SharedWorkPlaceMap {Value = value};
        }

        [Serializable]
        public class WorkPlaceMapIsEmpty : BaseSharedVariableChecker<SharedWorkPlaceMap>
        {
            protected override bool Check(SharedWorkPlaceMap variable)
            {
                return variable.Value.Count == 0;
            }
        }

        [Serializable]
        public class WorkPlaceMapCompareValue : BaseSharedVariableChecker<SharedWorkPlaceMap>
        {
            [RequiredField] public SharedWorkPlace key;
            [RequiredField] public float targetValue;
            public Condition condition;
            public enum Condition
            {
                less,
                greater,
                close
            }
            protected override bool Check(SharedWorkPlaceMap variable)
            {
                if (variable.TryGetValue(key.Value, out var value))
                {
                    switch (condition)
                    {
                        case Condition.close: return Mathf.Approximately(value, targetValue);
                        case Condition.less: return targetValue < value;
                        case Condition.greater: return targetValue > value;
                    }
                }
                return false;
            }
        }
        
        [Serializable]
        public class WokPlaceMapGet : BaseGetVariablePropertyValue<SharedWorkPlaceMap, SharedFloat>
        {
            [RequiredField] public SharedWorkPlace workPlace;
            protected override object GetValue(SharedWorkPlaceMap var)
            {
                if (var.TryGetValue(workPlace.Value, out var v)) return v;
                return 0;
            }
        }

        [Serializable]
        public class WorkPlaceMapUpdate : Action
        {
            public SharedWorkPlaceMap map;
            public SharedWorkPlace workPlace;
            public BehaviorFloatModifier modifier;

            public override TaskStatus OnUpdate()
            {
                map.TryGetValue(workPlace.Value, out var oldValue);
                var value = modifier.Apply(oldValue);
                value = Mathf.Max(value, 0);
                map.Set(workPlace.Value, value);
                return base.OnUpdate();
            }
        }
    }
}