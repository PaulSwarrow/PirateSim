using System;

namespace Services.AI.Interfaces
{
    public interface IBehaviorTreeNode
    {
        void Start();
        void Resume(IBehaviorContext context, Action callback);
        void Stop();
    }
}