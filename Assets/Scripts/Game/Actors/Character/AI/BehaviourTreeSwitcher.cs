using System;
using System.Collections.Generic;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSwitcher : IBehaviourTreeNode
    {
        public delegate bool Condition(Npc npc, bool resume);
        public List<(Condition condition, IBehaviourTreeNode handler)> cases;
        public IBehaviourTreeNode defaultBehaviour;

        private int lastChoice = -1;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            for (int i = 0; i < cases.Count; i++)
            {
                var item = cases[i];
                if (item.condition(npc, resume))
                {
                    Select(item.handler, i, resume, callback, npc);
                    return;
                }
            }

            Select(defaultBehaviour, -1, resume, callback, npc);
        }

        private void Select(IBehaviourTreeNode node, int index, bool resume, Action callback, Npc npc)
        {
            resume = resume && lastChoice == index;
            lastChoice = index;
            node.Execute(npc, callback, resume);
        }
    }
}