using System;
using DefaultNamespace;
using Game.Actors.Ship.Sails.Configs;
using Game.Actors.Ship.Sails.Data;
using Game.Actors.Ship.View;
using UnityEngine;

namespace Game.Actors.Ship.Sails
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
        public Rigidbody body { get; set; }

        public bool Jib => Config.Jib;
        public float Offset => Config.offset;


        public Vector3 GetSailPointMultiplied()
        {
            return parent.position +
                   parent.TransformDirection(new Vector3(
                       0,
                       AppManager.SailConstants.SailAngularDeviationEffect + body.centerOfMass.y,
                       Offset * AppManager.SailConstants.SailRotationMomentum));
        }

        public Vector3 GetNormaleVector()
        {
            return Quaternion.Euler(0, State.angle, 0) * (Jib ? Vector3.right : Vector3.forward);
        }

        public void Init()
        {
            Task = new SailGroupTask
            {
                angleIndex = Mathf.FloorToInt((float) Config.configuration.availableAngles.Length / 2),
                sailsUp = 0
            };
            view.model = this; //TODO better solution
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
                    var targetValue = Task.sailsUp > 0 ? Config.availableSails[i] : 0;
                    sail.value = Mathf.Lerp(sail.value, targetValue, Time.deltaTime);
                }
            }
        }
    }
}