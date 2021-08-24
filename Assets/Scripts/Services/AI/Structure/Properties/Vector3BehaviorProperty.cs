using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Services.AI.Structure
{
    public class Vector3BehaviorProperty : ObjectBehaviorProperty<Vector3>
    {
        public Vector3BehaviorProperty() : base()
        {
        }

        protected override void CreateProperties()
        {
            base.CreateProperties();
            SubProperty("magnitude", item => item.magnitude);
            SubProperty("x", item => item.x);
            SubProperty("y", item => item.y);
            SubProperty("z", item => item.z);
        }
    }
}