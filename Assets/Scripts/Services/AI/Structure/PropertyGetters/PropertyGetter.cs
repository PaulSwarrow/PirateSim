using System;
using Services.AI.Enums;
using Services.AI.Interfaces;
using UnityEngine.Assertions;

namespace Services.AI.Structure.PropertyGetters
{
    public class PropertyGetter<T> : IValidatable
    {
        public PropertySource source;
        //all data for any source:
        public T value;
        public string path;

        public T GetValue(IBehaviorContext context)
        {
            switch (source)
            {
                case PropertySource.Constant: return value;
                case PropertySource.Target: return context.Target.Read<T>(path);
                case PropertySource.Parent: return context.Current.Read<T>(path);
                default: throw new Exception($"Unknown property source {source}");
            }
        }

        public void Validate()
        {
            if(source != PropertySource.Constant) Assert.IsFalse(string.IsNullOrEmpty(path));
        }
    }
}