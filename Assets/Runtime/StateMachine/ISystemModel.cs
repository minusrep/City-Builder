using System.Collections.Generic;

namespace Runtime.StateMachine
{
    public interface ISystemModel
    {
        public Dictionary<string, object> Stats { get; }
    }
}