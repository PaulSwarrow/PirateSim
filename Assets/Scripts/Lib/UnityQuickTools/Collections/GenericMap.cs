using System;
using System.Collections.Generic;

public class GenericMap<TValue> : Dictionary<Type, TValue>
{
    public GenericMap(IEnumerable<TValue> items)
    {
        foreach (var item in items)
        {
            this[item.GetType()] = item;
        }
    }

    public GenericMap()
    {
    }

    public T Get<T>() where T : TValue
    {
        var type = typeof(T);
        return ContainsKey(type) ? (T) this[type] : default(T);
    }

    public void Set(TValue value)
    {
        this[value.GetType()] = value;
    }

    public bool Has<T>()
    {
        return ContainsKey(typeof(T));
    }

    public bool TryGetValue<T>(out T value) where T : TValue
    {
        if (TryGetValue(typeof(T), out var item))
        {
            value = (T) item;
            return true;
        }

        value = default;
        return false;
    }
}