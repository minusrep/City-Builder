using System.Collections.Generic;
using Runtime.Colony.ModelCollections;


namespace Runtime.Colony.Citizens.StateMachine.Descriptions
{
    public class CitizenStateDescriptionCollection : IDeserializeModel
    {
        private const string StatesKey = "states";
        public Dictionary<string, CitizenStateDescription> StateDescriptions { get; private set; } = new();

        public void Deserialize(Dictionary<string, object> data)
        {
            if (data[StatesKey] is not List<object> stateObjects)
            {
                throw new System.Exception("Invalid states dictionary");
            }
            
            foreach (var stateObject in stateObjects)
            {
                if (stateObject is not Dictionary<string, object> state)
                {
                    throw new System.Exception("Invalid state");
                }
                
                var stateDescription = new CitizenStateDescription();
                
                stateDescription.Deserialize(state);
                
                StateDescriptions.Add(stateDescription.Name, stateDescription);
            }
        }
    }
}