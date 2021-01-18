using System;
using System.Collections.Generic;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSwitcher : BaseBehaviourTreeCondition
    {
        public delegate bool Condition(Npc npc);

        public List<(Condition condition, IBehaviourTreeNode handler)> cases;
        public IBehaviourTreeNode defaultBehaviour;


        protected override IBehaviourTreeNode Select(Npc npc)
        {
            for (int i = 0; i < cases.Count; i++)
            {
                var item = cases[i];
                if (item.condition(npc))
                {
                    return item.handler;
                }
            }

            return defaultBehaviour;
        }
    }
}