using System.Collections.Generic;

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
        
    }
}