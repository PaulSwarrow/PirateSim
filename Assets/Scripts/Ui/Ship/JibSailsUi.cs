using System;
using Lib.Tools;
using ShipSystems;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Ui
{
    public class JibSailsUi : SailGroupUi
    {
        private void Awake()
        {
            
        }

        protected override void Update()
        {
            base.Update();
            icon.transform.localEulerAngles = -Vector3.forward * model.Options[model.Angle];
        }
    }
}