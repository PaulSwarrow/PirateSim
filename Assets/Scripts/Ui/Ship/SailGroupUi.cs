using System;
using Lib;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public abstract class SailGroupUi : BaseComponent
    {
        [SerializeField] protected SailStateUi icon;
        public SailGroup model { get; set; }
        public ShipEntity ship { get; set; }

        protected virtual void Update()
        {
            var influence = Vector3.Dot(ship.localWind, model.GetForceVector());
            if (Mathf.Abs(influence) >= model.minInfluence)
                icon.State = influence > 0 ? 1 : -1;
            else icon.State = 0;
        }
    }
}