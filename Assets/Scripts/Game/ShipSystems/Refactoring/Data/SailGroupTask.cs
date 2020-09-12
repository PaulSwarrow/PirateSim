using System;

namespace Game.ShipSystems.Refactoring
{
    [Serializable]
    public class SailGroupTask
    {
        public int sailsUp;
        public int angleIndex;

        public SailGroupTask Copy()
        {
            return new SailGroupTask
            {
                sailsUp = sailsUp,
                angleIndex = angleIndex
            };
        }

        public bool IsEqual(SailGroupTask value)
        {
            return value.sailsUp == sailsUp && value.angleIndex == angleIndex;
        }
    }
}