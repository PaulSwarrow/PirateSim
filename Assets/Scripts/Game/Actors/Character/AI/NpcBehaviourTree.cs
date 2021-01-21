using System.Collections;
using Game.Actors.Character.AI.Hardcode;
using UnityEngine;

namespace Game.Actors.Character.AI
{
    public class NpcBehaviourTree
    {
        public IBehaviourTreeNode rootNode = new ChillBTN();
        public Npc npc;

        //TODO downfall update for pause|resume option?
        //TODO stop|dispose feature?

        public IEnumerator Coroutine()
        {
            yield return null;
            rootNode.Start(npc);
            while (true)
            {
                var complete = false;
                rootNode.Resume(npc, () => complete = true);
                yield return new WaitUntil(() => complete);
            }
        }
    }
}