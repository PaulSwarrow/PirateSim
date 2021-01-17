using System;

namespace Game.Actors.Character.AI
{
    public interface IBehaviourTreeNode
    {
        void Execute(Npc npc, Action callback, bool resume);
    }
}