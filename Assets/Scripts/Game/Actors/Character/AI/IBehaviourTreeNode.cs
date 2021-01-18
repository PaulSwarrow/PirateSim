using System;

namespace Game.Actors.Character.AI
{
    public interface IBehaviourTreeNode
    {
        void Start(Npc npc);
        void Resume(Npc npc, Action callback);
        void Stop(Npc npc);
    }
}