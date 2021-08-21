using System;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI.Enums;
using Services.AI.Interfaces;

namespace Services.AI.Data.PropertyGetters
{
    public class PropertyGetter<T>
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
    }
}