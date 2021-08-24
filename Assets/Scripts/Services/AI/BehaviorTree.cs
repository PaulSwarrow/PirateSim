using System;
using Newtonsoft.Json;
using Services.AI;
using Services.AI.Interfaces;
using Services.AI.Structure;

namespace UnityEditor
{
    public class BehaviorTree<T> where T : IBehaviorDataProvider, new()
    {
        private T target = new T();
        [JsonProperty] private BehaviorData data;
        [JsonProperty] private BaseBehaviorTreeNode root;


        private BehaviorContext context;
        public void Start()
        {
            context = new BehaviorContext();
            context.Target = target;
            context.Current = target;

        }

        public void Resume(Action callback)
        {
            root.Resume(context, callback);

        }

        public void Stop()
        {
        }
        
    }
}