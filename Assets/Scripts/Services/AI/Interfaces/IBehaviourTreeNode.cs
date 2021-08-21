using System;

namespace Services.AI.Interfaces
{
    public interface IBehaviourTreeNode
    {
        void Start();
        void Resume(Action callback);
        void Stop();
    }
}