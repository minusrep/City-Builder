using System;
using System.Collections.Generic;

namespace Runtime.Colony.Citizens.StateMachine.Temp
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