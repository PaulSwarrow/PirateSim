using System;
using System.Linq;
using UnityEngine;

namespace Game.ShipSystems.Sails.Data
{
    [Serializable]
    public class SailGroupState
    {
        public float angle;
        public SailState[] sails = {new SailState()};
        public float inputWind;
        private bool jib;

        public float GetValue() => sails.Sum(item => item.value);
        
        public Vector3 GetNormaleVector()
        {
            return Quaternion.Euler(0, angle, 0) * (jib ? Vector3.right : Vector3.forward);
        }
    }
}