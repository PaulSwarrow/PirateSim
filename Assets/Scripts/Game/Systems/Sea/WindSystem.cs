using Game.Interfaces;
using UnityEngine;

namespace Game.Systems.Sea
{
    public class WindSystem : IGameSystem
    {
        public static Vector3 Wind;
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
            Wind = Quaternion.Euler(0, angle, 0) * Vector3.forward * force;
        }

    }
}