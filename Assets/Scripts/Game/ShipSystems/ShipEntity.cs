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
        [HideInInspector] public bool jib;
        public int Angle;
        [HideInInspector] public float Offset;
        public float[] Options = new float[0];



        public SailGroupView view;


        public Vector3 GetBaseVector()
        {
            return Quaternion.Euler(0, Options[Angle], 0) * (jib ? Vector3.right : Vector3.forward);
        }

        public static Vector3 GetBaseVector(float angle, bool jib)
        {
            return Quaternion.Euler(0, angle, 0) * (jib ? Vector3.right : Vector3.forward);
        }
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
        public Vector3 localWind { get; private set; }
        public Keel Keel => GetComponent<Keel>();

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
            localWind = _transform.InverseTransformVector(windSystem.Wind);
            var wind = windSystem.Wind;
            var shipForward = rigidbody.transform.forward;
            var shipRight = rigidbody.transform.right;
            var shipUp = transform.up;

            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailVector = sail.GetBaseVector();
                var windInfluence = Vector3.Dot(localWind, sailVector);

                var absInfluence = Mathf.Abs(windInfluence);
                var influenceSign = Mathf.Sign(windInfluence);
                if (sail.Value == 0 ||  absInfluence < sailsConfig.MinInfluence) continue;

                
                var resultForce = sailVector * (influenceSign * sailsConfig.WindForceMultiplier * Mathf.Sqrt(absInfluence));

                if (sail.jib)
                {
                    resultForce = Quaternion.Euler(Vector3.up * (- influenceSign* sailsConfig.jibsAngleCheat)) *
                        resultForce;
                    resultForce *= sailsConfig.JibsForceMultiplier;
                }
                resultForce = transform.TransformVector(resultForce);
                resultForce.y = 0;
                rigidbody.AddForceAtPosition(resultForce, point, ForceMode.Force); //optimize to sail groups!
                Debug.DrawRay(point,resultForce*Time.fixedDeltaTime, Color.yellow); //optimize to sail groups!
            }
        }

        private Vector3 GetSailPoint(SailGroup sailGroup)
        {
            return transform.position + transform.forward * sailGroup.Offset + Vector3.up * verticalOffset;
        }


        private void OnDrawGizmos()
        {
            if (sails.Count == 0) return;

            var self = transform;
            var shipPosition = self.position;
            shipPosition.y += 10;
            var shipForward = self.forward;
            var shipRight = self.right;
            var shipUp = transform.up;
            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailForward = self.TransformVector(sail.GetBaseVector());
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(point, shipUp * 4);
                Gizmos.color = Color.green;

                Gizmos.DrawRay(point, sailForward * 5);
            }
        }
    }
}