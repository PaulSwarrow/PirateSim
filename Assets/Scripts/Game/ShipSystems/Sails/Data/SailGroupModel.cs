using System;
using DefaultNamespace;
using Game.ShipSystems.Sails.Configs;
using ShipSystems;
using UnityEngine;

namespace Game.ShipSystems.Sails.Data
{
    [Serializable]
    public class SailGroupModel
    {
        public string name;
        public SailGroupView view;
        [HideInInspector] public ShipSailsConfig.SailGroupConfig Config;
        public SailGroupState State = new SailGroupState();
        public SailGroupTask Task;
        public Transform parent { get; set; }

        public bool Jib => Config.Jib;
        public float Offset => Config.offset;


        public Vector3 GetSailPointMultiplied()
        {
            return parent.position +
                   parent.forward * (Offset * AppManager.SailConstants.SailRotationMomentum) +
                   Vector3.up * (AppManager.SailConstants.SailAngularDeviationEffect);
        }

        public void Init()
        {
            Task = new SailGroupTask
            {
                angleIndex = Mathf.FloorToInt((float)Config.configuration.availableAngles.Length/2),
                sailsUp = 0
            };
        }

        public void Update()
        {
            //must be implemented outside of the model (by sail inputs)
            if (Task != null)
            {
                State.angle = Config.configuration.availableAngles[Task.angleIndex];
                for (int i = 0; i < State.sails.Length; i++)
                {
                    var sail = State.sails[i];
                    var sailMaxValue = Config.availableSails[i];
                    sail.value = i < Task.sailsUp ? sailMaxValue : 0;
                    
                    
                }
            }
        }
    }
}