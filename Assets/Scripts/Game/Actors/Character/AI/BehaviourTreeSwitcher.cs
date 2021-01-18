using System;
using System.Collections.Generic;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSwitcher : IBehaviourTreeNode
    {
        public InstantBTAction start;
        public delegate bool Condition(Npc npc);

        public List<(Condition condition, IBehaviourTreeNode handler)> cases;
        public IBehaviourTreeNode defaultBehaviour;


        private IBehaviourTreeNode lastCase;
        
        protected bool firstTime { get; private set; }

        public void Start(Npc npc)
        {
            firstTime = true;
            start?.Invoke(npc);
            
        }

        public void Resume(Npc npc, Action callback)
        {
            var behavior = Select(npc);

            if (behavior != lastCase)
            {
                lastCase?.Stop(npc);
                lastCase = behavior;
                behavior.Start(npc);
            }

            behavior.Resume(npc, callback);
            firstTime = false;
        }


        private IBehaviourTreeNode Select(Npc npc)
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

        public void Stop(Npc npc)
        {
            lastCase.Stop(npc);
            lastCase = null;
        }
    }
}