using System;

namespace Game.Actors.Character.AI
{
    public abstract class BaseBehaviourTreeCondition : IBehaviourTreeNode
    {
        public Predicate<Npc> Condition;
        public IBehaviourTreeNode BranchA;
        public IBehaviourTreeNode BranchB;

        //TODO separate properties and instance variables?


        private IBehaviourTreeNode lastSelection;
        protected bool Reset { get; private set; }

        public virtual void Start(Npc npc)
        {
            Reset = true;
        }

        public void Resume(Npc npc, Action callback)
        {
            var branch = Select(npc);
            if (lastSelection != branch)
            {
                lastSelection?.Stop(npc);
                branch.Start(npc);
            }

            lastSelection = branch;
            branch.Resume(npc, callback);
            Reset = false;
        }

        protected abstract IBehaviourTreeNode Select(Npc npc);

        public virtual void Stop(Npc npc)
        {
            lastSelection.Stop(npc);
            lastSelection = null;
        }
    }
}