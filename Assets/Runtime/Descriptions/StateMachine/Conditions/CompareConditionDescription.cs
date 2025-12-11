using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class CompareConditionDescription : ConditionDescription
    {
        private const string LesserOperationKey = "lesser";
        
        private const string EqualsOperationKey = "operation";
        
        private const string GreaterOperationKey = "greater";

        private const string CounterKey = "counter_name";
        
        private const string OperationKey = "operation_name";
        
        private const string ValueKey = "value";

        public CompareConditionDescription(Dictionary<string, object> data) : base(data)
        {
            Counter = data[CounterKey] as string;
            
            Operation = data[OperationKey] as string;

            Value = Convert.ToSingle(data[ValueKey]);
        }

        private string Counter { get; set; }

        private string Operation { get; set; }

        private float Value { get; set; }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IStatsModel statsModel)
            {
                return false;
            }
            
            var counterValue =  Convert.ToSingle(statsModel.Stats[Counter]);
            
            return Operation switch
            {
                GreaterOperationKey => counterValue > Value,
                EqualsOperationKey => Mathf.Approximately(counterValue, Value),
                LesserOperationKey => counterValue < Value,
                _ => false
            };
        }
    }
}