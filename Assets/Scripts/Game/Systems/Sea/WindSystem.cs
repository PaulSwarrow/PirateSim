using Game.Interfaces;
using UnityEngine;

namespace Game.Systems.Sea
{
    public class WindSystem : IGameSystem
    {
        public Vector3 Force { get; private set; } = Vector3.forward;
        public void Init()
        {
            
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void SetWind(float angle, float force)
        {
            Force = Quaternion.Euler(0, angle, 0) * Vector3.forward * force;
        }

    }
}