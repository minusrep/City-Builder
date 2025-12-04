using System.Collections.Generic;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class TempCitizenModel
    {
        public Dictionary<string, float> Counters;

        public Dictionary<string, bool> Flags;
        
        public TempCitizenModel()
        {
            Counters = new Dictionary<string, float>()
            {
                {"Hungry", 46},
                {"Energy", 84}
            };

            Flags = new Dictionary<string, bool>()
            {
                {"HasJob", true}
            };
        }
    }
}