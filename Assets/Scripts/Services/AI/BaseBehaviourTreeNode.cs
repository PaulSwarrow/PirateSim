using JsonSubTypes;
using Newtonsoft.Json;

namespace Services.AI
{
    [JsonConverter(typeof(JsonSubtypes), nameof(type))]
    [JsonSubtypes.KnownSubTypeAttribute(typeof(BehaviourTreeCondition), nameof(BehaviourTreeCondition))]
    public abstract class BaseBehaviourTreeNode
    {
        private string type => GetType().Name;
    }
}