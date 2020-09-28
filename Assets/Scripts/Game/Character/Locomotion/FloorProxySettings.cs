using System;
using UnityEngine;

namespace App.Character.Locomotion
{
    [Serializable]
    public class FloorProxySettings
    {
        public float groundRayLength = 0.3f;
        public float maxAngle = 45f;
        public float slopeMaxAngle = 70f;
    }
}