using UnityEditor;
using UnityEngine;

namespace App
{
    [CreateAssetMenu(fileName = "SailsConfig", menuName = "Game/SailsConfig", order = 1)]
    public class SailsConfig : ScriptableObject
    {
        public float WindForceMultiplier;
        public float JibsForceMultiplier;
        [Range(0, 0.9f)]public float MinInfluence = 0.2f;
        [Range(0, 45)]public float jibsAngleCheat = 10;
    }
}