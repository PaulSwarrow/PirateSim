using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Tools
{
    public static class CollectionExtension
    {
        public static T Shift<T>(this IList<T> collection)
        {
            if (collection.Count == 0) return default;
            var item = collection[0];
            collection.RemoveAt(0);
            return item;
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
        
    }
}