using System;
using System.Collections;

namespace Game.Actors.Character.AI
{
    public delegate void InstantBTAction(Npc npc, bool resume);
    public delegate void BTAction(Npc npc, Action callback, bool resume);
    public delegate IEnumerator BTCoroutine(Npc npc, bool resume);
    
    public class BehaviourTreeAction : IBehaviourTreeNode
    {
        public BTAction action;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            action(npc, callback, resume);
        }
    }
    
    
    public class BehaviourTreeInstantAction : IBehaviourTreeNode
    {
        public InstantBTAction action;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            action(npc, resume);
            callback();
        }
    }

    public class BehaviourTreeCoroutine : IBehaviourTreeNode
    {
        public BTCoroutine action;

        public void Execute(Npc npc, Action callback, bool resume)
        {
            GameManager.current.StartCoroutine(Coroutine(npc, callback, resume));
        }

        private IEnumerator Coroutine(Npc npc, Action callback, bool resume)
        {
            yield return action(npc, resume);
            callback();
        }
    }
}