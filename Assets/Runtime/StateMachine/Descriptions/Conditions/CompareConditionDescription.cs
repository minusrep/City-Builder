using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class CompareConditionDescription : ConditionDescription
    {
        private const string LesserOperationKey = "lesser";
        
        private const string EqualsOperationKey = "operation";
        
        private const string GreaterOperationKey = "greater";

        private const string CounterKey = "counter_name";
        
        private const string OperationKey = "operation_name";
        
        private const string ValueKey = "value";

        private string Counter { get; set; }

        private string Operation { get; set; }

        private float Value { get; set; }

        public override bool Check(Dictionary<string, object> model)
        {
            var counterValue =  Convert.ToSingle(model[Counter]);

            Debug.Log($"CounterValue: {counterValue}");
            
            return Operation switch
            {
                GreaterOperationKey => counterValue > Value,
                EqualsOperationKey => Mathf.Approximately(counterValue, Value),
                LesserOperationKey => counterValue < Value,
                _ => false
            };
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);

            Counter = data[CounterKey] as string;
            
            Operation = data[OperationKey] as string;

            Value = Convert.ToSingle(data[ValueKey]);
        }
    }
}