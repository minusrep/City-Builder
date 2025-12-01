using System;

namespace Runtime.Descriptions.Citizens
{
    [Serializable]
    public class ConditionDescription
    {
        public string Parameter;

        public string Operator;

        public object Value;
    }
}