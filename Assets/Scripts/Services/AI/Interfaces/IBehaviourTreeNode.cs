using System;

namespace Services.AI.Interfaces
{
    public interface IBehaviourTreeNode
    {
        void Start();
        void Resume(IBehaviourTreeContext context, Action callback);
        void Stop();
    }
}