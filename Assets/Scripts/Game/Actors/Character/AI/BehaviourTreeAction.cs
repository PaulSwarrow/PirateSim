using System;
using System.Collections;

namespace Game.Actors.Character.AI
{
    public delegate void InstantBTAction(Npc npc);
    public delegate void BTAction(Npc npc, Action callback);
    public delegate IEnumerator BTCoroutine(Npc npc);
    
    public class BehaviourTreeAction : IBehaviourTreeNode
    {
        public InstantBTAction start;
        public BTAction action;
        public InstantBTAction stop;

        public void Start(Npc npc)
        {
            start?.Invoke(npc);
        }

        public void Resume(Npc npc, Action callback)
        {
            action(npc, callback);
        }

        public void Stop(Npc npc)
        {
            stop?.Invoke(npc);
        }
    }
    
    
    public class BehaviourTreeInstantAction : IBehaviourTreeNode
    {
        public InstantBTAction start;
        public InstantBTAction action;
        public InstantBTAction stop;

        public void Start(Npc npc)
        {
            start?.Invoke(npc);
        }

        public void Resume(Npc npc, Action callback)
        {
            action.Invoke(npc);
            callback();
        }

        public void Stop(Npc npc)
        {
            stop?.Invoke(npc);
        }
    }

    public class BehaviourTreeCoroutine : IBehaviourTreeNode
    {
        public InstantBTAction start;
        public BTCoroutine action;
        public InstantBTAction stop;

        public void Start(Npc npc)
        {
            start?.Invoke(npc);
        }

        public void Resume(Npc npc, Action callback)
        {
            GameManager.current.StartCoroutine(Coroutine(npc, callback));
        }

        public void Stop(Npc npc)
        {
            stop?.Invoke(npc);
        }

        private IEnumerator Coroutine(Npc npc, Action callback)
        {
            yield return action(npc);
            callback();
        }
    }
}