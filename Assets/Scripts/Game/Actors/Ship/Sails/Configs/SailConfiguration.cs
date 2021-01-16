using UnityEngine;

namespace Game.Actors.Ship.Sails.Configs
{
    
    [CreateAssetMenu(fileName = "SailGroupConfig", menuName = "Game/SailGroupConfig", order = 10000)]
    public class SailConfiguration : ScriptableObject
    {
        public float[] availableAngles;
        public bool jib;
    }
}