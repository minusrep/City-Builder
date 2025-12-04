using System.Collections.Generic;
using Runtime.Colony.ModelCollections;

namespace Runtime.Colony.Citizens.StateMachine.Descriptions.Conditions
{
    public abstract class CitizenTransitionConditionDescription : IDeserializeModel
    {
        private const string TypeKey = "type";

        public string Type => _type;
        
        private string _type;

        public abstract bool Check(TempCitizenModel model);
        
        public virtual void Deserialize(Dictionary<string, object> data)
        {
            _type =  data[TypeKey] as string;
        }
    }
}