using UnityEngine;

namespace App
{
    public class WindSystem : GameSystem
    {
        public Vector3 Wind;
        public override void Start()
        {
            base.Start();
            // Wind = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * Random.Range(0.1f, 3);
        }

        public override void Update()
        {
            base.Update();
            // Wind = Quaternion.Euler(0, Random.Range(-.1f, .1f), 0) * Wind;
            // Wind *= Random.Range(0.99f, 1.01f);
        }

        public void SetWind(float angle, float force)
        {
            Wind = Quaternion.Euler(0, angle, 0) * Vector3.forward * force;
        }

    }
}