using System;
using Game.Actors.Ship.Sails;
using Game.Actors.Ship.View;
using Game.Systems.Sea;
using Lib;
using UnityEngine;

namespace Game.Actors.Ship
{


    [Serializable]
    public class SailGroup //split into config & state
    {
        public string Name;
        public int Value;
        [HideInInspector] public bool jib;
        public int Angle;
        [HideInInspector] public float Offset;
        public float[] Options = new float[0];


        public SailGroupView view;


        public Vector3 GetNormaleVector()
        {
            return Quaternion.Euler(0, Options[Angle], 0) * (jib ? Vector3.right : Vector3.forward);
        }

        public static Vector3 GetNormaleVector(float angle, bool jib)
        {
            return Quaternion.Euler(0, angle, 0) * (jib ? Vector3.right : Vector3.forward);
        }
    }


    public class ShipEntity : BaseComponent
    {
        private Vector3 floor;
        // private WindSystem windSystem;
        // private SailingConstantsConfig sailsConfig;

        
        //components:
        private Transform self;
        private Rigidbody rigidbody;
        private WindSystem windSystem;
        public ShipSailsController Sails { get; private set; }
        public Keel Keel { get; private set; }
        
        //values:
        public Vector3 localWind { get; private set; }
        public float LinearVelocity => Vector3.Dot(self.forward, rigidbody.velocity);
        public float AngularVelocity => rigidbody.angularVelocity.y;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            Keel = GetComponent<Keel>();
            Sails = GetComponent<ShipSailsController>();
            self = transform;
            windSystem = GameManager.current.GetSystem<WindSystem>();
            /*foreach (var group in sails)
            {
                group.view.model = group;
            }

            sailsConfig = GameManager.current.sailsConfig;*/
        }

        private void Update()
        {
            localWind = self.InverseTransformVector(WindSystem.Wind);
        }

        public void FullStop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            Sails.FullStop();
        }
    }
}