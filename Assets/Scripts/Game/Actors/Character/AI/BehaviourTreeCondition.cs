using System;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeCondition : BaseBehaviourTreeCondition
    {
        public Predicate<Npc> Condition;
        public IBehaviourTreeNode BranchA;
        public IBehaviourTreeNode BranchB;

        //TODO separate properties and instance variables?


        private IBehaviourTreeNode lastSelection;
        protected override IBehaviourTreeNode Select(Npc npc)
        {
            return Condition(npc) ? BranchA : BranchB;
        }
    }
}