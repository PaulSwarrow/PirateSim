using System;
using System.Collections.Generic;
using Game.Actors.Character;

namespace Game.Actors
{
    public static class ActorTracker<T> where T : IActor
    {
        private static readonly HashSet<T> List = new HashSet<T>();

        public static event Action<T> AddEvent; 
        public static event Action<T> RemoveEvent; 
        public static IReadOnlyCollection<T> All => List;

        public static void Register(T actor)
        {
            List.Add(actor);
        }

        public static void Unregister(T actor)
        {
            List.Remove(actor);
        }
    }
}