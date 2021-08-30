using System;
using BehaviorDesigner.Runtime;
using Lib.Navigation;

namespace Game.AI.BehaviorDesigner.Variables
{
    [Serializable]
    public class SharedNavPoint : SharedVariable<NavPoint>
    {

        public static implicit operator SharedNavPoint(NavPoint value)
        {
            return new SharedNavPoint {Value = value};
        }
    }
}