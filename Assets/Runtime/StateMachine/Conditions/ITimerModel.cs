using System.Collections.Generic;

namespace Runtime.Core
{
    public interface ITimerModel 
    {
        Dictionary<string, long> Timers { get; }
    }
}