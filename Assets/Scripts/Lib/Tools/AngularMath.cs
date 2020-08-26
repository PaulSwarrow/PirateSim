using UnityEngine;

namespace Lib.Tools
{
    public static class AngularMath
    {
        public static float Minify(float value)
        {
            value = value % 360;
            if (value > 180) value -= 360;
            if (value < -180) value += 360;
            return value;
        }
        public static Vector3 Minify(Vector3 value)
        {
            return new Vector3(Minify(value.x), Minify(value.y), Minify(value.z));
        }
        
    }
}