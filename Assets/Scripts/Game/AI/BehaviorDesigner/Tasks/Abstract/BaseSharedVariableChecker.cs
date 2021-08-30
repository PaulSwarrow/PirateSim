using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Game.AI.BehaviorDesigner.Tasks.Abstract
{
    public abstract class BaseSharedVariableChecker<T> : Action
    where T : SharedVariable
    {

        [RequiredField]
        public T variable;
        
        public override TaskStatus OnUpdate()
        {
            return Check(variable) ? TaskStatus.Success : TaskStatus.Failure;
        }

        protected abstract bool Check(T variable);
    }
}