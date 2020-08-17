using UnityEngine;

namespace Lib.Tools.Data
{
    public struct ClampedInt
    {
        private int max;
        private int min;
        private int current;

        public int Max
        {
            get=> max;
            set
            {
                max = value;
                if (current > value) current = value;
            }
        }
        public int Min
        {
            get=> min;
            set
            {
                max = value;
                if (current < value) current = value;
            }
        }
        public int Value
        {
            get=> current;
            set => current = Mathf.Clamp(value, min, max);
        }
    }
}