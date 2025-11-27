using System;

namespace Runtime.Colony.Citizens
{
    [Serializable]
    public class CitizenDescription
    {
        public string[] Names;

        /*public Dictionary<string, ResourceRange> ThresholdNeeds;*/
        
        public float StartMoveSpeed;
    }
}