using System.Collections;
using UnityEngine;

namespace Game.Actors.Character.AI
{
    public class NpcBehaviourTree
    {
        public IBehaviourTreeNode rootNode;
        private Npc npc;

        //TODO downfall update for pause|resume option?
        //TODO stop|dispose feature?

        public IEnumerator Start(Npc npc)
        {
            while (true)
            {
                var complete = false;
                rootNode.Execute(npc, () => complete = true, true);
                yield return new WaitUntil(() => complete);
            }
        }
    }
}