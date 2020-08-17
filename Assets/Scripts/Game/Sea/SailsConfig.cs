using UnityEditor;
using UnityEngine;

namespace App
{
    [CreateAssetMenu(fileName = "SailsConfig", menuName = "Game/SailsConfig", order = 1)]
    public class SailsConfig : ScriptableObject
    {
        public float WindForceMultiplier;
        public float JibsForceMultiplier;

    }
}