using System;
using System.Collections.Generic;

namespace Runtime.Descriptions.Citizens
{
    [Serializable]
    public class CitizensDescription
    {
        public List<string> Names;

        /*public Dictionary<string, ResourceRange> ThresholdNeeds;*/
        
        public float StartMoveSpeed;
    }
}