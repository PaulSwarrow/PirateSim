using System;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI.Interfaces;

namespace Services.AI
{
    [JsonConverter(typeof(JsonSubtypes), nameof(type))]
    [JsonSubtypes.KnownSubTypeAttribute(typeof(BehaviorTreeCondition), nameof(BehaviorTreeCondition))]
    public abstract class BaseBehaviorTreeNode : IBehaviorTreeNode, IValidatable
    {
        public abstract void Validate();
        private string type => GetType().Name;
        public void Start()
        {
            
        }

        public abstract void Resume(IBehaviorContext context, Action callback);
        
        public void Stop()
        {
        }
    }
}