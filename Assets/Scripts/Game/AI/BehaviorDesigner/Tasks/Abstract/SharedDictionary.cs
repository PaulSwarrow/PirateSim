using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Lib.UnityQuickTools.Collections;

namespace Game.AI.BehaviorDesigner.Tasks.Abstract
{
    public abstract class SharedDictionary<TKey, TValue> : SharedVariable<List<SharedDictionary<TKey, TValue>.Entry>>
    {
        public class Entry
        {
            public TKey key;
            public TValue value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (Value.TryFind(entry => entry.key.Equals(key), out var result))
            {
                value = result.value;
                return true;
            }

            value = default;
            return false;
        }


        public void Set(TKey key, TValue value)
        {
            if (Value.TryFind(entry => entry.key.Equals(key), out var result))
            {
                result.value = value;
            }
            else
            {
                Value.Add(new Entry
                {
                    key = key,
                    value = value
                });
            }
        }

        public void Add(TKey key, TValue value)
        {
            if(Value.Any(entry=> entry.key.Equals(key))) throw new Exception($"Key {key} has already been added");
            Value.Add(new Entry
            {
                key = key,
                value = value
            });
        }
    }
}