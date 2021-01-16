using System;
using System.Linq;

namespace Game.Actors.Ship.Sails.Data
{
    [Serializable]
    public class SailGroupState
    {
        public float angle;
        public SailState[] sails = {new SailState()};
        public float GetValue() => sails.Sum(item => item.value);
    }
}