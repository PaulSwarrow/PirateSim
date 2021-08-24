using System;
using Services.AI.Interfaces;
using Services.AI.Structure;

namespace Services.AI
{
    public class TaskBehaviorNode: BaseBehaviorTreeNode
    {
        public BehaviorData data;
        public BaseBehaviorTreeNode root;
        public override void Validate()
        {
            
        }

        public override void Resume(IBehaviorContext context, Action callback)
        {
            root.Resume(context, callback);
        }
    }
}