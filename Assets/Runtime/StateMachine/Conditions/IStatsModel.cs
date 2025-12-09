using System.Collections.Generic;

namespace Runtime.StateMachine.Conditions
{
    public interface IStatsModel
    {
        Dictionary<string, float> Stats { get; }
    }
}