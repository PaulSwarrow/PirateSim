using System;
using UnityEngine;

namespace ShipSystems.Sim
{
    public enum Axis
    {
        x,
        y,
        z
    }

    public static class VectorTools
    {
        public static float GetAxis(this Vector3 vector, Axis axis)
        {
            switch (axis)
            {
                case Axis.x: return vector.x;
                case Axis.y: return vector.y;
                case Axis.z: return vector.z;
                default: return 0;
            }
        }
    }
}