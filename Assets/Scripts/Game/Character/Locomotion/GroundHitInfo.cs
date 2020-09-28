using UnityEngine;
using UnityEngine.UI;

namespace App.Character.Locomotion
{
    public class GroundHitInfo
    {
        public Rigidbody rigidbody;
        public float excessiveDistance;
        public Vector3 normale;
        public bool steady;
    }
    
    
    public struct SphereHit
    {
        public Rigidbody Rigidbody;
        public bool Hit;
        public Vector3 Normale;
        public float ExcessiveDistance;

    }

    public enum RayHit
    {
        none, slope, step
        
    }
}