using System.Collections.Generic;

namespace Runtime.Colony.Citizens.StateMachine
{
    public interface ISerializeModel
    {
        Dictionary<string, object> Serialize();
    }
}