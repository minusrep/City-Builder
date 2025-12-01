using System;

namespace Runtime.Descriptions.Citizens
{
    [Serializable]
    public class CitizensDescription
    {
        public string[] Names;

        /*public Dictionary<string, ResourceRange> ThresholdNeeds;*/
        
        public float StartMoveSpeed;
    }
}