using System;

namespace Runtime.Colony.Citizens
{
    [Serializable]
    public class TransitionDescription
    {
        public string ToState;
        
        public ConditionDescription[] ConditionDescriptions;
    }
}