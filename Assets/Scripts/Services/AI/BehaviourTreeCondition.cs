using Services.AI.Data;
using Services.AI.Enums;

namespace Services.AI
{
    public class BehaviourTreeCondition : BaseBehaviourTreeNode
    {
        public class Option
        {
            public Condition condition;
            public BaseBehaviourTreeNode node;
        }


        public Option[] options;

    }
}