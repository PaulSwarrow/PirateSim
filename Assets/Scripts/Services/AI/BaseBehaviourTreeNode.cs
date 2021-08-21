using System;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI.Interfaces;

namespace Services.AI
{
    [JsonConverter(typeof(JsonSubtypes), nameof(type))]
    [JsonSubtypes.KnownSubTypeAttribute(typeof(BehaviourTreeCondition), nameof(BehaviourTreeCondition))]
    public abstract class BaseBehaviourTreeNode : IBehaviourTreeNode, IValidatable
    {
        public abstract void Validate();
        private string type => GetType().Name;
        public void Start()
        {
            
        }

        public abstract void Resume(IBehaviourTreeContext context, Action callback);

        public void Stop()
        {
        }
    }
}