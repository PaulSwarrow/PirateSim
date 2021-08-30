using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Game.AI.BehaviorDesigner.Tasks.Abstract
{
    public abstract class BaseGetVariablePropertyValue<TVar, TValue> : Action
    where TValue : SharedVariable
    {
        [RequiredField]
        public TVar target;
        [RequiredField]
        public TValue storeResult;

        public override TaskStatus OnUpdate()
        {
            storeResult.SetValue(GetValue(target));
            return base.OnUpdate();
        }

        protected abstract object GetValue(TVar target);
    }
}