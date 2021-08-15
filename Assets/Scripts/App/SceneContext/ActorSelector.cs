using System;
using System.Collections.Generic;
using Game.Actors.Character;
using Game.Actors.Ship;
using Lib.UnityQuickTools.Collections;
using Random = UnityEngine.Random;

namespace App.SceneContext
{
    public class ActorSelector<T> where T : BaseActor<T>
    {
        private HashSet<T> unprocessed = new HashSet<T>();
        private Action<T> addListener;


        public IReadOnlyCollection<T> All => ActorTracker<T>.All;
        public ActorSelector()
        {
            ActorTracker<T>.All.Foreach(actor => unprocessed.Add(actor));
            ActorTracker<T>.AddEvent += OnAdded;
        }

        public void Dispose()
        {
            ActorTracker<T>.AddEvent -= OnAdded;
            unprocessed.Clear();
            unprocessed = null;
        }

        public void Track(Action<T> handler)
        {
            ActorTracker<T>.All.Foreach(handler);
            addListener = handler;

        }

        public bool GetRandom(out T item)
        {
            item = default;

            if (All.Count == 0) return false;

            var selected = Random.Range(0, All.Count);
            var i = 0;
            foreach (var actor in All)
            {
                if (i == selected)
                {
                    item = actor;
                    break;
                }

                i++;
            }

            return true;
        }
        
        private void OnAdded(T item)
        {
            addListener?.Invoke(item);

        }
        private void OnRemoved(T item)
        {
            
        }

        public void Process(Action<T> handler)
        {
            unprocessed.Foreach(handler);
            unprocessed.Clear();
        }
    }
}