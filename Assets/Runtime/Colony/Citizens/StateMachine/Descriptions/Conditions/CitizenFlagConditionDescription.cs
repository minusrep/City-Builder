using System.Collections.Generic;

namespace Runtime.Colony.Citizens.StateMachine.Descriptions.Conditions
{
    public class CitizenFlagConditionDescription : CitizenTransitionConditionDescription
    {
        private const string FlagNameKey = "flagName";
        private const string FlagStatusKey = "flagStatus";
        
        private string FlagName { get; set; }
        
        private bool FlagStatus { get; set; }
        
        public override bool Check(TempCitizenModel model)
        {
            return model.Flags[FlagName] == FlagStatus;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            
            FlagName = data[FlagNameKey] as string;
            
            FlagStatus = (bool) data[FlagStatusKey];
        }
    }
}