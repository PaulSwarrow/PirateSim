using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Lib.Tools
{
    public class GameObjectsPool<T> where T : MonoBehaviour
    {
        private Transform container;
        private List<T> items = new List<T>();

        public GameObjectsPool(Transform container)
        {
            this.container = container;
        }

        public void Add(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.parent = container;
            items.Add(item);
        }

        public bool Extract(Transform to, out T item)
        {
            if (items.Count > 0)
            {
                item = items.Shift();
                item.transform.parent = to;
                item.gameObject.SetActive(true);
                return true;
            }

            item = default;
            return false;
        }
        
    }
}