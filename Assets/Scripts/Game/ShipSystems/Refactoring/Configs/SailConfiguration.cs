using System;
using System.Diagnostics.Contracts;
using UnityEngine;

namespace Game.ShipSystems.Refactoring
{
    
    [CreateAssetMenu(fileName = "SailGroupConfig", menuName = "Game/SailGroupConfig", order = 10000)]
    public class SailConfiguration : ScriptableObject
    {
        public float[] availableAngles;
        public bool jib;
    }
}