using System;
using System.Collections.Generic;
using Runtime.Colony;

namespace Runtime.Descriptions.Citizens
{
    public class CitizenTransitionConditionDescription : IDeserializeModel
    {
        private const string TypeKey = "type";

        private string _type;

        public bool Check()
        {
            //TODO: Connect Citizen Model to this condition
            
            throw new NotImplementedException();
        }
        
        public void Deserialize(Dictionary<string, object> data)
        {
            _type =  data[TypeKey] as string;
        }
    }
}