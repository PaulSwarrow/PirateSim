using System;
using BehaviorDesigner.Runtime;
using Game.Actors.Character.Interactions;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;

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
    }
}