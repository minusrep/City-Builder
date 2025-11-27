using System;

namespace Runtime.Colony.Citizens
{
    public class TransitionDescription
    {
        public string ToState;
        
        public ConditionDescription[] ConditionDescriptions;
    }
}