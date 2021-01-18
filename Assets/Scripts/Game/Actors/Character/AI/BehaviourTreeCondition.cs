using System;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeCondition : IBehaviourTreeNode
    {
        public Predicate<Npc> Condition;
        public IBehaviourTreeNode BranchA;
        public IBehaviourTreeNode BranchB;

        //TODO separate properties and instance variables?


        private IBehaviourTreeNode lastSelection;
        public void Start(Npc npc)
        {
        }

        public void Resume(Npc npc, Action callback)
        {
            var branch = Condition(npc) ? BranchA : BranchB;
            if (lastSelection != branch)
            {
                lastSelection?.Stop(npc);
                branch.Start(npc);
            }
            
            lastSelection = branch;
            branch.Resume(npc, callback);
        }

        public void Stop(Npc npc)
        {
            if (lastSelection != null)
            {
                lastSelection.Stop(npc);
                lastSelection = null;
            }
        }
    }
}