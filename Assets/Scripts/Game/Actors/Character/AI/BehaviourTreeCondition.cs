using System;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeCondition : IBehaviourTreeNode
    {
        public Predicate<Npc> Condition;
        public IBehaviourTreeNode BranchA;
        public IBehaviourTreeNode BranchB;

        //TODO separate properties and instance variables?


        public void Execute(Npc npc, Action callback)
        {
            var currentBranch = Condition(npc) ? BranchA : BranchB;
            currentBranch.Execute(npc, callback);
        }
    }
}