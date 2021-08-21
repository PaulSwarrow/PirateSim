using System;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI.Enums;
using Services.AI.Interfaces;
using UnityEngine.Assertions;

namespace Services.AI.Data.PropertyGetters
{
    public class PropertyGetter<T> : IValidatable
    {
        public PropertySource source;
        //all data for any source:
        public T value;
        public string path;

        public T GetValue(IBehaviourTreeContext context)
        {
            switch (source)
            {
                case PropertySource.Constant: return value;
                case PropertySource.Context: return context.GetProperty<T>(path);
                default: throw new Exception($"Uknown property source {source}");
            }
        }

        public void Validate()
        {
            if(source == PropertySource.Context) Assert.IsFalse(string.IsNullOrEmpty(path));
        }
    }
}