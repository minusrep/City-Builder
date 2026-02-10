using System.Collections.Generic;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface ITimerModel 
    {
        Dictionary<string, long> Timers { get; }
    }
}