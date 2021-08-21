using System;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI.Data.Comarisons;
using Services.AI.Data.PropertyGetters;
using Services.AI.Interfaces;

namespace Services.AI.Data
{
    
    [JsonConverter(typeof(JsonSubtypes), nameof(valueType))]
    [JsonSubtypes.KnownSubTypeAttribute(typeof(Condition<int, IntComparison>), nameof(Int32))]
    public abstract class Condition: IValidatable
    {
        protected abstract string valueType { get; }

        public abstract bool Check(IBehaviourTreeContext context);
        public abstract void Validate();
    }

    internal class Condition<TValue, TComparison> : Condition
        where TComparison : IComparison<TValue>
    {
        
        protected override string valueType => typeof(TValue).Name;

        public PropertyGetter<TValue> a;
        public PropertyGetter<TValue> b;
        public TComparison comparison;

        public override bool Check(IBehaviourTreeContext context)
        {
            return comparison.Check(a.GetValue(context), b.GetValue(context));
        }

        public override void Validate()
        {
            a.Validate();
            b.Validate();
        }
    }
}