using System;
using System.Collections.Generic;
using App;
using Game.ShipSystems.Sails.Configs;
using Game.ShipSystems.Sails.Data;
using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

namespace Game.ShipSystems.Sails
{
    public class ShipSailsController : BaseComponent
    {
        //configuration:
        public ShipSailsConfig config;

        //cache:
        public List<SailGroupModel> sails;
        private Transform self;
        private Rigidbody rigidbody;
        private SailingConstantsConfig sailingConstants;

        //values:
        private Vector3 localWind;

        private void Start()
        {
            self = transform;
            sailingConstants = GameManager.current.sailsConfig;
            rigidbody = GetComponent<Rigidbody>();

            foreach (var sailConfig in config.sails)
            {
                if (sails.TryFind(item => item.name == sailConfig.name, out var model))
                {
                    model.parent = self;
                    model.Config = sailConfig;
                    //TODO optimize by serializable dictionary!
                    model.Init();
                }
                else Debug.LogError("Sail Group Model not found :" + sailConfig.name, this);
            }
        }

        private void Update()
        {
            sails.ForEach(item => item.Update());
        }

        private void FixedUpdate()
        {
            localWind = self.InverseTransformVector(WindSystem.Wind);

            foreach (var sail in sails)
            {
                var point = sail.GetSailPointMultiplied();
                var sailVector = sail.GetNormaleVector();
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
                Debug.DrawRay(point + Vector3.up*5, resultForce * Time.fixedDeltaTime, Color.yellow);
            }
        }

        public void FullStop()
        {
            foreach (var sailGroupModel in sails)
            {
                sailGroupModel.Task.sailsUp = 0;
            }
        }

        public void ApplyOrders(IEnumerable<SailOrder> orders)
        {
            //TODO animations && npc-s work
            foreach (var sailOrder in orders)
            {
                sailOrder.sails.Task = sailOrder.task;
            }
        }
    }
}