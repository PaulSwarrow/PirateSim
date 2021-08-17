using System;
using Game.Actors.Character;
using Game.Actors.Ship.Sails;
using Game.Actors.Ship.View;
using Game.Interfaces;
using Game.Systems.Sea;
using Lib;
using Lib.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Actors.Ship
{
    public class ShipActor : SceneActor<ShipActor>
    {
        private Vector3 floor;
        // private WindSystem windSystem;
        // private SailingConstantsConfig sailsConfig;


        //components:
        private Transform self;
        private Rigidbody rigidbody;
        public ShipSailsController Sails { get; private set; }
        public Keel Keel { get; private set; }
        public DynamicNavMeshSurface NavSurface { get; private set; }

        //values:
        public Vector3 LocalWind { get; set; }
        public float LinearVelocity => Vector3.Dot(self.forward, rigidbody.velocity);
        public float AngularVelocity => rigidbody.angularVelocity.y;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            Keel = GetComponent<Keel>();
            Sails = GetComponent<ShipSailsController>();
            NavSurface = GetComponent<DynamicNavMeshSurface>();
            self = transform;
        }
        

        public void FullStop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            Sails.FullStop();
        }
    }
}