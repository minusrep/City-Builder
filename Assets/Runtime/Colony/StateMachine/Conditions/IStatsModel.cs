using Runtime.Colony.Stats.Collections;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IStatsModel
    {
        StatModelCollection Stats { get; }
    }
}