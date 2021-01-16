using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Lib.UnityQuickTools.Collections
{
    public static class CollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                collection.Add(element);
            }
        }
        
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> handler)
        {
            foreach (var element in collection)
            {
                handler(element);
            }
        }
        
        
        public static T Shift<T>(this List<T> list)
        {
            T item = default(T);
            if (list.Count > 0)
            {
                item = list[0];
                list.RemoveAt(0);
            }

            return item;
        }

        public static T Pop<T>(this List<T> list)
        {
            T item = default(T);
            if (list.Count > 0)
            {
                var i = list.Count - 1;
                item = list[i];
                list.RemoveAt(i);
            }

            return item;
        }

        public static bool TrueForAny<T>(this List<T> list, Predicate<T> condition)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (condition(list[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : default(T);
        }

        public static bool TryFind<T>(this IEnumerable<T> list, Predicate<T> predicate, out T result)
        {
            foreach (var item in list)
            {
                if (!predicate(item)) continue;
                result = item;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryFindIndex<T>(this List<T> list, Predicate<T> predicate, out int index)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (!predicate(item)) continue;
                index = i;
                return true;
            }

            index = -1;
            return false;
        }

        public static List<T> ExtractAll<T>(this List<T> list, Predicate<T> predicate)
        {
            var result = new List<T>();
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (!predicate(item)) continue;
                result.Add(item);
                list.RemoveAt(i);
            }

            return result;
        }


        public static int Least<T>(this IList<T> collection, Func<T, float> selectBy)
        {
            var result = -1;
            var min = float.MaxValue;
            for (var i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                var value = selectBy(item);
                if (value < min)
                {
                    result = i;
                    min = value;
                }
            }

            return result;
        }

        public static int Most<T>(this IList<T> collection, Func<T, float> selectBy)
        {
            var result = -1;
            var max = float.MinValue;
            for (var i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                var value = selectBy(item);
                if (value >= max)
                {
                    result = i;
                    max = value;
                }
            }

            return result;
        }

        public static T LeastOrDefault<T>(this IEnumerable<T> collection, Func<T, float> selectBy)
        {
            var min = float.MaxValue;
            T result = default;
            foreach (var item in collection)
            {
                var value = selectBy(item);
                if (value < min)
                {
                    min = value;
                    result = item;
                }
            }

            return result;
        }
    }
}