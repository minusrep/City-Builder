using System.Collections.Generic;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IStatsModel
    {
        Dictionary<string, float> Stats { get; }
    }
}