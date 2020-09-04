using System;

namespace DefaultNamespace
{
    public interface ITimeSpanTracker
    {
        TimeSpan TimeSpan { get; }
        string TimeSpanString { get; }
    }
}