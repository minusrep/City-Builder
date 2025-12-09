using System.Collections.Generic;

namespace Runtime.Core
{
    public interface IStatsModel
    {
        Dictionary<string, float> Stats { get; }
    }
}