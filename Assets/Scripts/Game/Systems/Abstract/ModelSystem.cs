using System;
using System.Collections.Generic;
using Game.Actors;
using Game.Actors.Character;
using Game.Interfaces;
using Lib.UnityQuickTools.Collections;

namespace Game.Systems.Abstract
{
    public abstract class ModelSystem<T>: IGameSystem, IGameUpdateSystem, IGamePhysicsSystem
        where T : IDisposable, new()
    {
        private HashSet<T> list = new HashSet<T>();
        public virtual void Init()
        {
            
        }

        public void Start()
        {
            list.Foreach(OnStart);
        }

        public void Stop()
        {
        }

        public void Update()
        {
            list.Foreach(OnUpdate);
        }

        public void FixedUpdate()
        {
            list.Foreach(OnFixedUpdate);
        }

        protected T Create()
        {
            var item = new T();
            list.Add(item);
            return item;
        }

        protected void Remove(T item)
        {
            item.Dispose();
            list.Remove(item);
        }

        protected virtual void OnStart(T actor)
        {
            
        }

        protected virtual void OnUpdate(T actor)
        {
            
        }
        protected virtual void OnFixedUpdate(T actor)
        {
            
        }
    }
}