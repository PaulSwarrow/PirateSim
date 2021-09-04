using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace DI
{
    public class DependencyContainer
    {
        private Dictionary<Type, object> map = new Dictionary<Type, object>();
        private DependencyContainer parent;
        private bool hasParent;

        public DependencyContainer(DependencyContainer parent = null)
        {
            this.parent = parent;
            hasParent = parent != null;

        }
        public void Register<T>(T item) where T : class
        {
            var type = typeof(T);
            Assert.IsNotNull(item, $"Register null for {type} type");
            map.Add(type, item);
        }

        public void InjectDependencies()
        {
            foreach (var item in map.Values)
            {
                var itemType = item.GetType();
                foreach (var field in ReflectionTools.GetFieldsWithAttributes(itemType, typeof(InjectAttribute)))
                {
                    if (TryGetValue(field.FieldType, out var value))
                    {
                        field.SetValue(item, value);
                    }
                }

                foreach (var property in ReflectionTools.GetPropsWithAttributes(itemType, typeof(InjectAttribute)))
                {
                    if (TryGetValue(property.PropertyType, out var value))
                    {
                        property.SetValue(item, value);
                    }
                }
            }
        }

        private bool TryGetValue(Type type, out object value)
        {
            if (map.TryGetValue(type, out value)) return true;
            if (hasParent && parent.TryGetValue(type, out value)) return true;
            value = default;
            return false;
        }

        public T GetItem<T>()
        {
            return (T)map[typeof(T)];
        }

        public void Dispose()
        {
            foreach (var item in map.Values)
            {
                var itemType = item.GetType();
                foreach (var field in ReflectionTools.GetFieldsWithAttributes(itemType, typeof(InjectAttribute)))
                {
                    field.SetValue(item, null);
                }

                foreach (var property in ReflectionTools.GetPropsWithAttributes(itemType, typeof(InjectAttribute)))
                {
                    if (map.TryGetValue(property.PropertyType, out var value))
                    {
                        property.SetValue(item, null);
                    }
                }
            }

            map.Clear();
            map = null;
        }
    }
}