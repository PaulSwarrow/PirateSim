using System;
using System.Collections.Generic;
using DefaultNamespace;
using HutongGames.PlayMaker.Actions;
using Newtonsoft.Json;
using Services.AI.Interfaces;
using Assert = UnityEngine.Assertions.Assert;

namespace Services.AI.Structure
{
    public class BehaviorData : IBehaviorDataProvider
    {
        [JsonProperty] protected Dictionary<string, BehaviorProperty> properties = new Dictionary<string, BehaviorProperty>();
        
        public T Read<T>(string path)
        {
            Assert.IsFalse(string.IsNullOrEmpty(path));
            PropertyPathUtil.Parse(ref path, out var name);
            return properties[name].Read<T>(path);
        }
        
        public void Write<T>(string path, T value)
        {
            Assert.IsFalse(string.IsNullOrEmpty(path));
            PropertyPathUtil.Parse(ref path, out var name);
            properties[name].Write(path, value);
        }

    }

    public abstract class BehaviorDataProxy : BehaviorData
    {

        protected BehaviorDataProxy()
        {
            CreateProperties();
        }

        protected abstract void CreateProperties();
        

        protected ObjectBehaviorProperty<T> Property<T>(string name)
        {
            var property = new ObjectBehaviorProperty<T>();
            properties.Add(name, property);
            return property;
        }
    }
}