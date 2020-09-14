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
        public float GetValue() => sails.Sum(item => item.value);
    }
}