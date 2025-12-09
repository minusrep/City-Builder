using System.Collections.Generic;

namespace Runtime.StateMachine.Conditions
{
    public interface IFlagsModel 
    {
        Dictionary<string, bool> Flags { get; }
    }
}