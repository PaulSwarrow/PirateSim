using System;
using System.Linq;
using Lib.UnityQuickTools.Collections;
using Services.AI.Enums;
using Services.AI.Interfaces;
using Services.AI.Structure;
using UnityEngine.Assertions;

namespace Services.AI
{
    public class BehaviorTreeCondition : BaseBehaviorTreeNode
    {
        public class Option : IValidatable
        {
            public Condition condition;
            public BaseBehaviorTreeNode node;
            public void Validate()
            {
                condition.Validate();
                node.Validate();
            }
        }


        public Option[] options;

        public override void Validate()
        {
            Assert.AreNotEqual(0, options.Length);
            options.Foreach(option=> option.Validate());
        }

        public override void Resume(IBehaviorContext context, Action callback)
        {
            
            foreach (var option in options)
            {
                if (option.condition.Check(context))
                {
                    option.node.Resume(context, callback);
                    return;
                }
            }

            options.Last().node.Resume(context, callback);
        }
    }
}