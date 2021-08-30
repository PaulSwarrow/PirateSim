using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.BehaviorDesigner.Data
{
    [Serializable]
    public class BehaviorFloatModificator
    {
        public enum Operation
        {
            Set,
            Add,
            Multiply,
            NotLess,
            NotGreater
        }

        public Operation operation;

        public float value;

        public float Apply(float input)
        {
            switch (operation)
            {
                case Operation.Set: return value;
                case Operation.Add: return input + value;
                case Operation.Multiply: return input * value;
                case Operation.NotLess: return Mathf.Max(value, input);
                case Operation.NotGreater: return Mathf.Min(value, input);
                default: throw new Exception("Unknown operation");

            }
        }

    }

    [Serializable]
    public class FloatModifiers 
    {
        public List<BehaviorFloatModificator> modificators;
        public float Apply(float input)
        {
            var value = input;
            for (int i = 0; i < modificators.Count; i++)
            {
                value = modificators[i].Apply(value);

            }

            return value;
        }
    }
}