using System.Collections.Generic;

namespace Runtime.StateMachine.Conditions
{
    public interface ITimerModel 
    {
        Dictionary<string, long> Timers { get; }
    }
}