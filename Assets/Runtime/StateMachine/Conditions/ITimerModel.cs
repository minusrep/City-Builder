using System.Collections.Generic;

namespace Runtime.Core
{
    public interface ITimerModel 
    {
        Dictionary<string, ulong> Timers { get; }
    }
}