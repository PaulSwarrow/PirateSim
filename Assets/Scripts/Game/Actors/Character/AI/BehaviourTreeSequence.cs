using System;
using System.Collections;
using UnityEngine.Assertions.Must;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSequence : IBehaviourTreeNode
    {
        public IBehaviourTreeNode[] sequence;

        private int index;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            if (!resume) index = 0;

            var step = sequence[index];
            index = index < sequence.Length - 1 ? index + 1 : 0;
            step.Execute(npc, callback, sequence.Length == 1);
        }
    }

    public class BehaviourTreeStrongSequence : IBehaviourTreeNode
    {
        public IBehaviourTreeNode[] sequence;

        private int index;
        private Npc npc;
        private Action callback;

        public void Execute(Npc npc, Action callback, bool resume)
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
                step.Execute(npc, Next, sequence.Length == 1);
            }
            else
            {
                callback();
            }
        }
    }
}