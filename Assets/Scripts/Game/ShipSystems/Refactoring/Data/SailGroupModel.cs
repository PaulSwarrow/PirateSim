using App;
using DefaultNamespace;
using ShipSystems;
using UnityEngine;

namespace Game.ShipSystems.Refactoring
{
    public class SailGroupModel
    {
        public string Name;
        public ShipSailsConfig.SailGroupConfig Config;
        public SailGroupState State;
        public SailGroupTask GroupTask;
        public Transform parent;

        public bool Jib => Config.Jib;
        public float Offset => Config.offset;


        public Vector3 GetSailPointMultiplied()
        {
            return parent.position +
                   parent.forward * (Offset * AppManager.SailConstants.SailRotationMomentum) +
                   Vector3.up * (AppManager.SailConstants.SailAngularDeviationEffect);
        }
    }
}