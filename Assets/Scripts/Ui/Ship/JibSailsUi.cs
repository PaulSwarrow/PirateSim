using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Ui
{
    public class JibSailsUi : SailGroupUi
    {
        protected override void Update()
        {
            base.Update();
            icon.transform.localEulerAngles = -Vector3.forward * model.Options[model.Angle];
        }
    }
}