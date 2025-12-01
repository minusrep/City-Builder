using System.Collections.Generic;
using Runtime.Descriptions.Citizens;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateDescriptionCollection
    {
        public Dictionary<string, CitizenStateDescription> StateDescriptions { get; }
        public CitizenStateDescriptionCollection(Dictionary<string, CitizenStateDescription> stateDescriptions)
        {
            StateDescriptions = stateDescriptions;
        }
    }
}