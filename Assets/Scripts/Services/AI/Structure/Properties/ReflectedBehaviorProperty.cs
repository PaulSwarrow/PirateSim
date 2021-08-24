using System;
using System.Reflection;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine.Assertions;

namespace Services.AI.Structure
{
    //TODO do not use. Temp super-slow solution 
    public class ReflectedBehaviorProperty<TValue> : BehaviorProperty
    {
        [JsonProperty] private TValue cachedValue;

        public override T Read<T>(string path)
        {
            return ReadRecursive<T>(cachedValue, path);
        }

        public override void Write<T>(string path, T value)
        {
            if (string.IsNullOrEmpty(path))
            {
                SetValue(value);
            }
            else
            {
                WriteRecursive(cachedValue, path, value);
            }
        }


        private T GetValue<T>()
        {
            if (cachedValue is T value) return value;
            throw new Exception("Mistype");
        }

        private void SetValue<T>(T value)
        {
            if (value is TValue tvalue) 
                cachedValue = tvalue;
            else
                throw new Exception("Mistype");
        }

        private T ReadRecursive<T>(object obj, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return (T) obj;
            }

            PropertyPathUtil.Parse(ref path, out var name);
            var child = obj.GetType().GetProperty(name)?.GetValue(obj);
            return ReadRecursive<T>(child, path);
        }

        private void WriteRecursive<T>(object obj, string path, T value)
        {
            Assert.IsFalse(string.IsNullOrEmpty(path));
            PropertyPathUtil.Parse(ref path, out var name);
            var property = obj.GetType().GetProperty(name);
            Assert.IsNotNull(property);
            if (string.IsNullOrEmpty(path))
            {
                property.SetValue(obj, value);
            }
            else
            {
                WriteRecursive(property.GetValue(obj), path, value);
            }
        }
    }
}