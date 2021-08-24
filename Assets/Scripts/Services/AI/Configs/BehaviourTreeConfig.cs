using Newtonsoft.Json;
using Services.AI.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Services.AI.Configs
{
    public abstract class BehaviourTreeConfig<T> : ScriptableObject , ISerializationCallbackReceiver
        where T : IBehaviorDataProvider, new()
    {
        [SerializeField] private string data;
        public BehaviorTree<T> tree;
        
        
        public void OnBeforeSerialize()
        {
            if(tree == null) return;
            data = JsonConvert.SerializeObject(tree);
        }

        public void OnAfterDeserialize()
        {
            if(string.IsNullOrEmpty(data)) return;
            tree = JsonConvert.DeserializeObject<BehaviorTree<T>>(data);
        }

        private void OnValidate()
        {
            OnAfterDeserialize();
        }
    }
}