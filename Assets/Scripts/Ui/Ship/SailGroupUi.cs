using System;
using Lib;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public abstract class SailGroupUi : BaseComponent
    {
        [SerializeField] protected SailStateUi icon;
        public SailGroup model;
        protected virtual void Update()
        {
            if (Mathf.Abs(model.currentInfluence) >= model.minInfluence)
                icon.State = model.currentInfluence > 0 ? 1 : -1;
            else icon.State = 0;
        }
    }
}