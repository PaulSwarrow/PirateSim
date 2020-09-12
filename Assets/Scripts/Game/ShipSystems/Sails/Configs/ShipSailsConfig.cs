using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ShipSystems.Sails.Configs
{
    [CreateAssetMenu(fileName = "ShipSailsConfig", menuName = "Game/ShipSailsConfig", order = 10000)]
    public class ShipSailsConfig : ScriptableObject
    {
        [Serializable]
        public class SailGroupConfig
        {
            public string name;
            public SailConfiguration configuration;
            public float offset;
            public bool Jib => configuration.jib;
            public float[] availableSails = {1};

            public float GetAngle(int angleIndex) => configuration.availableAngles[angleIndex];
        }

        public List<SailGroupConfig> sails;
    }
}