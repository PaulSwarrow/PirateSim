using UnityEditor;
using UnityEngine;

namespace App
{
    [CreateAssetMenu(fileName = "SailsConfig", menuName = "Game/SailsConfig", order = 1)]
    public class SailsConfig : ScriptableObject
    {
        public float WindForceMultiplier;
        public float JibsForceMultiplier;
        [Range(0, 0.9f)]public float MinWindCatch = 0.2f;//refactor to min angle
        [Range(0, 45)]public float jibsAngleCheat = 10;
        [Min(0)] public float SailAngularDeviationEffect = 1;
        [Min(0)] public float SailRotationMomentum = 1;
    }
}