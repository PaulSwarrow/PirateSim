using System;
using System.Collections;
using UnityEngine.Assertions.Must;

namespace Game.Actors.Character.AI
{
    public class BehaviourTreeSequence : IBehaviourTreeNode
    {
        public IBehaviourTreeNode[] sequence;

        private int index;
        private IBehaviourTreeNode lastSelection;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            if (!resume) index = 0;

        }

        public void Start(Npc npc)
        {
            index = 0;
        }

        public void Resume(Npc npc, Action callback)
        {
            lastSelection?.Stop(npc);

            var step = sequence[index];
            lastSelection = step;
            step.Start(npc);
            index = index < sequence.Length - 1 ? index + 1 : 0;
            step.Resume(npc, callback);
        }

        public void Stop(Npc npc)
        {
            lastSelection?.Stop(npc);
            lastSelection = null;
        }
    }

}