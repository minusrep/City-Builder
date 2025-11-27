using System;

namespace Runtime.Colony.Citizens
{
    [Serializable]
    public class ConditionDescription
    {
        public string Parameter;

        public string Operator;

        public object Value;
    }
}