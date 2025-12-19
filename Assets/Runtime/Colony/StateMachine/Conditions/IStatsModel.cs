using System.Collections.Generic;
using Runtime.Colony.Stats;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IStatsModel
    {
        StatModelCollection Stats { get; }
    }
}