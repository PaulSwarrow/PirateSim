using System.ComponentModel;
using UnityEngine;

namespace Lib.Tools
{
    public class LocalFactory<T> where T : MonoBehaviour
    {
        private T prefab;
        private GameObjectsPool<T> pool;
        private Transform container;
        
        public LocalFactory(Transform container)
        {
            this.container = container;
            pool = new GameObjectsPool<T>(container);
            var children = container.GetComponentsInChildren<T>();
            prefab = children[0];
            prefab.gameObject.SetActive(false);
            for (var i = 1; i < children.Length; i++)
            {
                pool.Add(children[i]);
            }
        }

        public T Create()
        {
            if (!pool.Extract(container, out var item))
            {
                item = Object.Instantiate(prefab, container);
                item.gameObject.SetActive(true);
            }

            return item;
        }

        private void Remove(T item)
        {
            pool.Add(item);
        }
 
    }
}