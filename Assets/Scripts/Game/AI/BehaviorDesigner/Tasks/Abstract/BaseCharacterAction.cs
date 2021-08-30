using BehaviorDesigner.Runtime.Tasks;
using Game.AI.BehaviorDesigner.Variables;

namespace Game.AI.BehaviorDesigner.Tasks.Abstract
{
    public abstract class BaseCharacterAction : Action
    {
        [RequiredField] public SharedCharacterActor actor;

    }
}