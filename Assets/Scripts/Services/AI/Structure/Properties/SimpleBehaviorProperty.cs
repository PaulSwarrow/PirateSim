using System;
using UnityEngine.Assertions;

namespace Services.AI.Structure
{
    public class SimpleBehaviorProperty<TValue> : BehaviorProperty
    {
        private TValue cachedValue;

        
        public SimpleBehaviorProperty()
        {
            
        }
        
        public override T Read<T>(string path)
        {
            Assert.IsTrue(path.Length == 0);
            if (cachedValue is T value) return value;
            throw new Exception("Types mismatch");
        }

        public override void Write<T>(string path, T value)
        {
            Assert.IsTrue(path.Length == 0);
            if (value is TValue tValue) cachedValue = tValue;
            throw new Exception("Types mismatch");
        }
    }
}