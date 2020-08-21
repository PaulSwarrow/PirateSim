using System;
using System.Collections.Generic;
using Lib;
using Lib.Tools;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public abstract class SailGroupUi : BaseComponent
    {

        private List<SailStateUi> states = new List<SailStateUi>();
        public SailGroup Model
        {
            get => model;
            set
            {
                model = value;
                foreach (var option in model.Options)
                {
                    var item = factory.Create();
                    item.Jib = model.jib;
                    item.Angle = option;
                    states.Add(item);
                }
            }
        }

        public ShipEntity Ship { get; set; }

        private LocalFactory<SailStateUi> factory;
        private SailGroup model;
        private void Awake()
        {
            factory = new LocalFactory<SailStateUi>(transform);
        }

        protected virtual void Update()
        {
            Debug.Log(name);
            foreach (var item in states)
            {
                var influence = Vector3.Dot(Ship.localWind, SailGroup.GetForceVector(item.Angle, model.jib));
                if (Mathf.Abs(influence) >= model.minInfluence)
                    item.State = influence > 0 ? 1 : -1;
                else item.State = 0;
                
                item.Active = Math.Abs(item.Angle - model.Angle) < 0.1f;
            }
            
        }
    }
}