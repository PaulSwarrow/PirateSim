using UnityEngine;

namespace Game.Actors.Ship.Sails
{
    public static class SailMath
    {
        public static Vector3 GetNormaleVector(float angle, bool jib)
        {
            return Quaternion.Euler(0, angle, 0) * (jib ? Vector3.right : Vector3.forward);
        }
        
    }
}