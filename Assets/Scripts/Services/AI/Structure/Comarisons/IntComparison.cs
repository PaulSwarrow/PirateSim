using System;
using Services.AI.Enums;
using Services.AI.Interfaces;

namespace Services.AI.Data.Comarisons
{
    internal class IntComparison : IComparison<int>
    {
        public ComparisonOperator operation;

        public bool Check(int a, int b)
        {
            switch (operation)
            {
                case ComparisonOperator.Equal: return a == b;
                case ComparisonOperator.NotEqual: return a != b;
                case ComparisonOperator.More: return a > b;
                case ComparisonOperator.Less: return a < b;
                default: throw new Exception($"Unknown comparison operation:{operation}");
            }
        }
    }
}