using System;
using BehaviorDesigner.Runtime;
using Game.Actors.Character.Interactions;
using Game.Actors.Workplaces;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;
using Lib.UnityQuickTools.Enums;

namespace Game.AI.BehaviorDesigner.Workplaces
{
    [Serializable]
    public class SharedWorkPlace : SharedVariable<WorkPlace>
    {
        public static implicit operator SharedWorkPlace(WorkPlace value)
        {
            return new SharedWorkPlace {Value = value};
        }

        public class WorkplaceIsEmpty : BaseSharedVariableChecker<SharedWorkPlace>
        {
            protected override bool Check(SharedWorkPlace variable)
            {
                return !variable.Value.Occupied;
            }
        }
        
        public class WorkplaceGetEnterPoint: BaseGetVariablePropertyValue<SharedWorkPlace, SharedNavPoint>
        {
            protected override object GetValue(SharedWorkPlace target)
            {
                return target.Value.EnterPosition;
            }
        }
        
        public class WorkplaceGet: BaseGetVariablePropertyValue<SharedWorkPlace, SharedFloat>
        {
            public WorkPlaceParameter property;
            protected override object GetValue(SharedWorkPlace target)
            {
                return target.Value.GetParameter(property);
            }
        }
        
        public class WorkplaceIs: BaseGetVariablePropertyValue<SharedWorkPlace, SharedBool>
        {
            public WorkPlaceTag mask;
            public EnumComparison comparison; 
            protected override object GetValue(SharedWorkPlace target)
            {
                return target.Value.Check(mask, comparison);
            }
        }
    }
}