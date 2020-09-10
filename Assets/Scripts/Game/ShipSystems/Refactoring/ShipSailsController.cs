using System;
using System.Collections.Generic;
using App;
using DefaultNamespace;
using Lib;
using UnityEngine;

namespace Game.ShipSystems.Refactoring
{
    public class ShipSailsController : BaseComponent
    {
        public class SailLink
        {
            public string name;
            
        }
        [SerializeField] private ShipSailsConfig config;

        private List<SailGroupModel> sails;
        private Transform self;
        private Rigidbody rigidbody;
        private WindSystem windSystem;
        private SailingConstantsConfig sailingConstants;
        private Vector3 localWind;

        private void Start()
        {
            self = transform;

            windSystem = GameManager.current.GetSystem<WindSystem>();
            sailingConstants = GameManager.current.sailsConfig;
            rigidbody = GetComponent<Rigidbody>();
            
            
        }

        private void FixedUpdate()
        {
            localWind = self.InverseTransformVector(windSystem.Wind);

            foreach (var sail in sails)
            {
                var point = sail.GetSailPointMultiplied();
                var sailVector = sail.State.GetNormaleVector();
                var windInfluence = Vector3.Dot(localWind, sailVector);

                var absInfluence = Mathf.Abs(windInfluence);
                var influenceSign = Mathf.Sign(windInfluence);
                var sailValue = sail.State.GetValue();
                if (Math.Abs(sailValue) < 0.1f || absInfluence < sailingConstants.MinWindCatch) continue;


                var resultForce = sailVector *
                                  (influenceSign * 
                                   sailingConstants.WindForceMultiplier * 
                                   Mathf.Sqrt(absInfluence) *
                                   sailValue);

                if (sail.Jib)
                {
                    resultForce.x *= 1 - sailingConstants.jibsCheat;
                    resultForce *= sailingConstants.JibsForceMultiplier;
                }

                resultForce = self.TransformVector(resultForce);
                resultForce.y = 0;
                rigidbody.AddForceAtPosition(resultForce, point, ForceMode.Force); 
                Debug.DrawRay(point, resultForce * Time.fixedDeltaTime, Color.yellow); 
            }
        }
    }
}