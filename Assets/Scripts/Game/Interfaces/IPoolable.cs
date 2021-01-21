using UnityEngine;

namespace Game.Interfaces
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDispose();
    }
}