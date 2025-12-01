using System;

namespace Runtime.Descriptions.Citizens
{
    [Serializable]
    public class TransitionDescription
    {
        public string ToState;
        
        public ConditionDescription[] ConditionDescriptions;
    }
}