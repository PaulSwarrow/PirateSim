using System;
using Lib;
using UnityEngine;

namespace ShipSystems
{
    [RequireComponent(typeof(Rigidbody))]
    public class Keel : BaseComponent
    {
        private Rigidbody rigidbody;
        [SerializeField] [Range(0, 1)] private float sideDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float backDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float angularDrag = 0.5f;
        
        [SerializeField] [Range(-90, 90)] private float wheel = 0;
        [SerializeField] private float wheelInfluence = 1;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var localSpeed = transform.InverseTransformDirection(rigidbody.velocity);
            localSpeed.x -= localSpeed.x * sideDrag;
            if(localSpeed.z < 0)  localSpeed.z -= localSpeed.z * backDrag;
            rigidbody.velocity = transform.TransformDirection(localSpeed);

            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.y -= angularVelocity.y * angularDrag;
            
            rigidbody.AddTorque(0, wheel*localSpeed.z * wheelInfluence/Time.fixedDeltaTime, 0, ForceMode.Force);
            
            //todo static angular drag wheel influence
        }
        
    }
}