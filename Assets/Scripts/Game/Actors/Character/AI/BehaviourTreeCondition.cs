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
        public void Execute(Npc npc, Action callback, bool resume)
        {
            
            var branch = Condition(npc) ? BranchA : BranchB;
            resume = resume && lastSelection == branch;
            lastSelection = branch;
            branch.Execute(npc, callback, resume);
        }
    }
}