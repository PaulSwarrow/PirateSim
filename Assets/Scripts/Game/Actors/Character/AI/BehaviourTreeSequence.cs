using System;
using System.Collections;
using UnityEngine.Assertions.Must;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSequence : IBehaviourTreeNode
    {
        public IBehaviourTreeNode[] sequence;

        private int index;

        public void Execute(Npc npc, Action callback)
        {
            var step = sequence[index];
            index = index < sequence.Length - 1 ? index + 1 : 0;
            step.Execute(npc, callback);
        }
    }
    
    public class BehaviourTreeStrongSequence : IBehaviourTreeNode
    {
        public IBehaviourTreeNode[] sequence;

        private int index;
        private Npc npc;
        private Action callback;
        public void Execute(Npc npc, Action callback)
        {
            this.npc = npc;
            this.callback = callback;
            Next();
        }

        private void Next()
        {
            if (index < sequence.Length)
            {
                var step = sequence[index];
                index++;
                step.Execute(npc, Next);
            }
            else
            {
                callback();
            }
        }
    }
    
}