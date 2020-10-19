using System;
using System.Collections.Generic;
using Lib.UnityQuickTools.Collections;

namespace App.Tools
{
    public class GenericEventDispatcher
    {
        private abstract class Listener
        {
        }

        private class Listener<T> : Listener
        {
            private HashSet<Action<T>> list = new HashSet<Action<T>>();

            public void Add(Action<T> action)
            {
                list.Add(action);
            }

            public void Remove(Action<T> action)
            {
                list.Remove(action);
            }

            public void Invoke(T data)
            {
                foreach (var action in list)
                {
                    action.Invoke(data);
                }
            }
        }

        private Dictionary<Type, Listener> map = new Dictionary<Type, Listener>();

        public void Dispatch<T>(T data)
        {
            if (TryGetListener<T>(out var listener))
            {
                listener.Invoke(data);
            }
        }

        public void Subscribe<T>(Action<T> action)
        {
            var listener = GetListener<T>();
            listener.Add(action);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            if (TryGetListener<T>(out var listener))
            {
                listener.Remove(action);
            }
        }

        private bool TryGetListener<T>(out Listener<T> listener)
        {
            var t = typeof(T);
            if (map.TryGetValue(t, out var item))
            {
                listener = (Listener<T>) item;
                return true;
            }

            listener = default;
            return false;
        }

        private Listener<T> GetListener<T>()
        {
            var t = typeof(T);
            if (map.TryGetValue(t, out var item))
            {
                return (Listener<T>) item;
            }

            var listener = new Listener<T>();
            map[t] = listener;
            return listener;
        }
    }
}