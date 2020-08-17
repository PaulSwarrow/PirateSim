using System;
using System.Collections.Generic;
using App;
using Lib;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace ShipSystems
{
    public enum SailDirection
    {
        Forward = 0,
        Right = -45,
        Left = 45,
        Straight = 90
    }


    [Serializable]
    public class SailGroup
    {
        public string Name;
        public int Value;
        public bool jib;
        public SailDirection Angle;
        public float Offset;
        [NonSerialized] public float[] Potential = new[] {0, 0.5f, 1};

        public int MaxValue => Potential.Length;

        public SailGroupView view;
    }


    public class ShipEntity : BaseComponent
    {
        [SerializeField] private List<SailGroup> sails;

        private Vector3 floor;

        [SerializeField] private Rigidbody rigidbody;
        private WindSystem windSystem;
        private SailsConfig sailsConfig;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            foreach (var group in sails)
            {
                group.view.model = group;
            }

            windSystem = GameManager.current.GetSystem<WindSystem>();
            sailsConfig = GameManager.current.sailsConfig;
        }

        private void Update()
        {
            ApplyWind(windSystem.Wind);
            KeelWork();
        }

        private void KeelWork()
        {
            var localSpeed = transform.InverseTransformDirection(rigidbody.velocity);
            localSpeed.x *= 0.6f;
            rigidbody.velocity = transform.TransformDirection(localSpeed);
        }

        public void ApplyWind(Vector3 wind)
        {
            var shipForward = rigidbody.transform.forward;
            var shipRight = rigidbody.transform.right;
            var shipUp = transform.up;

            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailForward = GetSailDirection(sail);
                var windInfluence = Vector3.Dot(wind, sailForward);
                var sailPotential = sail.Potential[sail.Value];

                var resultForce = sailForward * (windInfluence * sailPotential * sailsConfig.WindForceMultiplier);
                if (sail.jib) resultForce *= sailsConfig.JibsForceMultiplier;
                rigidbody.AddForceAtPosition(resultForce, point, ForceMode.Force); //optimize to sail groups!
            }
        }

        private Vector3 GetSailPoint(SailGroup sailGroup)
        {
            return transform.position + transform.forward * sailGroup.Offset + transform.up * 5;
        }

        private Vector3 GetSailDirection(SailGroup sailGroup)
        {
            return transform.TransformDirection(Quaternion.Euler(0, (int) sailGroup.Angle, 0) *
                                                 (sailGroup.jib ? Vector3.right : Vector3.forward)).normalized;
        }

        private void OnDrawGizmos()
        {
            var self = transform;
            var shipPosition = self.position;
            shipPosition.y += 10;
            var shipForward = self.forward;
            var shipRight = self.right;
            var shipUp = transform.up;
            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailForward = GetSailDirection(sail);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(point, shipUp * 4);
                Gizmos.color = Color.green;

                Gizmos.DrawRay(point, sailForward * 5);
            }
        }
    }
}