using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Game.Actors.Character.Interactions;
using Game.AI.BehaviorDesigner.Data;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
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


        public class WorkPlaceMapIsEmpty : BaseSharedVariableChecker<SharedWorkPlaceMap>
        {
            protected override bool Check(SharedWorkPlaceMap variable)
            {
                return variable.Value.Count == 0;
            }
        }

        public class WorkPlaceMapCompareValue : BaseSharedVariableChecker<SharedWorkPlaceMap>
        {
            protected override bool Check(SharedWorkPlaceMap variable)
            {
                return variable.Value.Count == 0;
            }
        }
        
        public class WokPlaceMapGet : BaseGetVariablePropertyValue<SharedWorkPlaceMap, SharedFloat>
        {
            [RequiredField] public SharedWorkPlace workPlace;
            protected override object GetValue(SharedWorkPlaceMap var)
            {
                if (var.TryGetValue(workPlace.Value, out var v)) return v;
                return 0;
            }
        }

        public class WorkPlaceMapUpdate : Action
        {
            public SharedWorkPlaceMap map;
            public SharedWorkPlace workPlace;
            public FloatModifiers modificator;

            public override TaskStatus OnUpdate()
            {
                map.TryGetValue(workPlace.Value, out var oldValue);
                map.Set(workPlace.Value, modificator.Apply(oldValue));
                return base.OnUpdate();
            }
        }
    }
}