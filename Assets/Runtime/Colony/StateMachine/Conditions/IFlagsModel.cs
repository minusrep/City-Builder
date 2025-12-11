using System.Collections.Generic;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IFlagsModel 
    {
        Dictionary<string, bool> Flags { get; }
    }
}