using System;
using System.Collections.Generic;
using App;
using Lib;
using UnityEngine;

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
        [SerializeField] public List<SailGroup> sails;
        [SerializeField] private float verticalOffset = 5;

        private Vector3 floor;

        [SerializeField] private Rigidbody rigidbody;
        private WindSystem windSystem;
        private SailsConfig sailsConfig;

        private Transform self;
        public Vector3 localWind { get; private set; }
        public Keel Keel => GetComponent<Keel>();
        public float LinearVelocity => Vector3.Dot(self.forward, rigidbody.velocity);
        public float AngularVelocity => rigidbody.angularVelocity.y;

        private void Start()
        {
            self = transform;
            foreach (var group in sails)
            {
                group.view.model = group;
            }

            windSystem = GameManager.current.GetSystem<WindSystem>();
            sailsConfig = GameManager.current.sailsConfig;
        }

        private void FixedUpdate()
        {
            localWind = self.InverseTransformVector(windSystem.Wind);

            foreach (var sail in sails)
            {
                var point = GetSailPointMultiplied(sail);
                var sailVector = sail.GetNormaleVector();
                var windInfluence = Vector3.Dot(localWind, sailVector);

                var absInfluence = Mathf.Abs(windInfluence);
                var influenceSign = Mathf.Sign(windInfluence);
                if (sail.Value == 0 || absInfluence < sailsConfig.MinWindCatch) continue;


                var resultForce =
                    sailVector * (influenceSign * sailsConfig.WindForceMultiplier * Mathf.Sqrt(absInfluence));

                if (sail.jib)
                {
                    resultForce = Quaternion.Euler(Vector3.up * (-influenceSign * sailsConfig.jibsAngleCheat)) *
                                  resultForce;
                    resultForce *= sailsConfig.JibsForceMultiplier;
                }

                resultForce = self.TransformVector(resultForce);
                resultForce.y = 0;
                rigidbody.AddForceAtPosition(resultForce, point, ForceMode.Force); //optimize to sail groups!
                Debug.DrawRay(point, resultForce * Time.fixedDeltaTime, Color.yellow); //optimize to sail groups!
            }
        }

        private Vector3 GetSailPoint(SailGroup sailGroup)
        {
            return self.position + self.forward * sailGroup.Offset + Vector3.up * verticalOffset;
        }

        private Vector3 GetSailPointMultiplied(SailGroup sailGroup)
        {
            return self.position + self.forward * (sailGroup.Offset * sailsConfig.SailRotationMomentum) +
                   Vector3.up * (verticalOffset * sailsConfig.SailAngularDeviationEffect);
        }


        private void OnDrawGizmos()
        {
            if (sails.Count == 0) return;

            self = transform;
            var shipPosition = self.position;
            shipPosition.y += 10;
            var shipForward = self.forward;
            var shipRight = self.right;
            var shipUp = transform.up;
            foreach (var sail in sails)
            {
                var point = GetSailPoint(sail);
                var sailForward = self.TransformVector(sail.GetNormaleVector());
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(point, shipUp * 4);
                Gizmos.color = Color.green;

                Gizmos.DrawRay(point, sailForward * 5);
            }
        }
    }
}