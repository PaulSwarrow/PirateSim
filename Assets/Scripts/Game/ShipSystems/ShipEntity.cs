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
    public class SailGroup //split into config & state
    {
        public string Name;
        public int Value;
        public bool jib;
        public int Angle;
        public float Offset;
        public float[] Options = new float[0];
        [Range(0, 1)] public float minInfluence = 0.1f;
        [NonSerialized] public float[] Potential = new[] {0, 0.5f, 1};

        public int MaxValue => Potential.Length;
        public float currentInfluence;
        

        public SailGroupView view;
    }


    public class ShipEntity : BaseComponent
    {
        [SerializeField] public List<SailGroup> sails;
        [SerializeField] private float verticalOffset = 5;

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

        private void FixedUpdate()
        {
            var wind = windSystem.Wind;
            var shipForward = rigidbody.transform.forward;
            var shipRight = rigidbody.transform.right;
            var shipUp = transform.up;

            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailForward = GetSailDirection(sail);
                var windInfluence = Vector3.Dot(wind, sailForward);
                var sailPotential = sail.Potential[sail.Value];

                if (Mathf.Abs(windInfluence) < sail.minInfluence)
                {
                    sail.currentInfluence = 0;
                    continue;
                }
                sail.currentInfluence = windInfluence;
                
                var resultForce = sailForward * (windInfluence * sailPotential * sailsConfig.WindForceMultiplier);
                if (sail.jib) resultForce *= sailsConfig.JibsForceMultiplier;
                rigidbody.AddForceAtPosition(resultForce, point, ForceMode.Force); //optimize to sail groups!
            }
        }

        private Vector3 GetSailPoint(SailGroup sailGroup)
        {
            return transform.position + transform.forward * sailGroup.Offset + Vector3.up * verticalOffset;
        }

        private Vector3 GetSailDirection(SailGroup sailGroup)
        {
            var dir = Quaternion.Euler(0,  sailGroup.Options[sailGroup.Angle], 0) * (sailGroup.jib ? Vector3.right : Vector3.forward);
            dir = transform.TransformDirection(dir).normalized;
            dir.y = 0;
            return dir;
        }

        private void OnDrawGizmos()
        {
            if(sails.Count ==0) return;
            
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