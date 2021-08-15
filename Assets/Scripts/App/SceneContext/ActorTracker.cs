using System;
using System.Collections.Generic;
using Game.Actors.Character;
using Lib.UnityQuickTools.Collections;
using Random = UnityEngine.Random;

namespace App.SceneContext
{
    public static class ActorTracker<T> where T : BaseActor<T>
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