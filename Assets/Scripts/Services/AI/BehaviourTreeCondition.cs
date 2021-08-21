using System;
using System.Linq;
using Lib.UnityQuickTools.Collections;
using Services.AI.Data;
using Services.AI.Enums;
using Services.AI.Interfaces;
using UnityEngine.Assertions;

namespace Services.AI
{
    public class BehaviourTreeCondition : BaseBehaviourTreeNode
    {
        public class Option : IValidatable
        {
            public Condition condition;
            public BaseBehaviourTreeNode node;
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

        public override void Resume(IBehaviourTreeContext context, Action callback)
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