using UnityEngine;

namespace Game.Tools
{
    public static class Geometry
    {
        public static Vector3 GetRandomForward() => Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;
    }
}