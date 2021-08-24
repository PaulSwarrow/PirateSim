using System;
using System.Collections.Generic;
using DefaultNamespace;
using Services.AI.Interfaces;

namespace Services.AI.Structure
{
    public abstract class BehaviorProperty : IBehaviorDataProvider
    {
        public abstract T Read<T>(string path);

        public abstract void Write<T>(string path, T value);
    }

    public class ObjectBehaviorProperty<TValue> : BehaviorProperty
    {
        private readonly Func<TValue> getter;
        private readonly Action<TValue> setter;
        protected Dictionary<string, BehaviorProperty> properties { get; } = new Dictionary<string, BehaviorProperty>();
        private TValue cachedValue;

        public ObjectBehaviorProperty(Func<TValue> getter, Action<TValue> setter)
        {
            this.getter = getter;
            this.setter = setter;
            CreateProperties();
        }
        
        public ObjectBehaviorProperty()
        {
            this.getter = () => cachedValue;
            this.setter = value => cachedValue = value;
            CreateProperties();
        }
        
        public override T Read<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                var localValue = getter();
                if (localValue is T value) return value;
                throw new Exception("Types mismatch");
            }
            else
            {
                PropertyPathUtil.Parse(ref path, out var name);
                var property = GetProperty(name);
                return property.Read<T>(path);
            }
        }


        public override void Write<T>(string path, T value)
        {
            if (string.IsNullOrEmpty(path))
            {
                if (value is TValue tValue) setter(tValue);
                throw new Exception("Types mismatch");
            }
            else
            {
                PropertyPathUtil.Parse(ref path, out var name);
                var property = GetProperty(name);
                property.Write(path, value);
            }
        }

        protected virtual void CreateProperties()
        {
            
        }

        protected void SubProperty<T>(string name, Func<TValue, T> get, Action<TValue, T> set)
        {
            properties.Add(name, new ObjectBehaviorProperty<T>(
                () => get(Value),
                value => set(Value, value))
            );
        }
        protected void SubProperty<T>(string name, Func<TValue, T> get)
        {
            properties.Add(name, new ObjectBehaviorProperty<T>(
                () => get(Value),
                null)
            );
        }

        public TValue Value
        {
            get => getter();
            set => setter(value);
        } 

        private BehaviorProperty GetProperty(string name)
        {
            return properties[name];
        }
    }
}