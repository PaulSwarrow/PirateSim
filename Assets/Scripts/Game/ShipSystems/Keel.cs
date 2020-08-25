using System;
using Lib;
using UnityEngine;

namespace ShipSystems
{
    [RequireComponent(typeof(Rigidbody))]
    public class Keel : BaseComponent
    {
        private Rigidbody rigidbody;
        private Transform self;
        [SerializeField]  private float sideDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float backDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float angularDrag = 0.5f;

        [SerializeField] [Range(-90, 90)] public float wheel = 0;
        [SerializeField] private float wheelInfluence = 1;

        private void Awake()
        {
            self = transform;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            
            var right = self.right;
            right.y = 0;
            right.Normalize();
            var forward = self.forward;
            forward.y = 0;
            forward.Normalize();
            var sideVelocity = Vector3.Dot(right, rigidbody.velocity);
            var linearVelocity = Vector3.Dot(forward, rigidbody.velocity);

            var drag = right * (sideVelocity * sideDrag);
            if (linearVelocity < 0) drag += forward * (linearVelocity * backDrag);
            Debug.Log(sideVelocity+", "+rigidbody.velocity.z);

            rigidbody.AddForce(-drag, ForceMode.VelocityChange);

            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.y -= angularVelocity.y * angularDrag;

            rigidbody.AddRelativeTorque(0, wheel * linearVelocity * wheelInfluence / Time.fixedDeltaTime, 0, ForceMode.Force);

            //todo static angular drag wheel influence
        }
    }
}