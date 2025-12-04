using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.Citizens.StateMachine.Descriptions.Conditions
{
    public class CitizenCompareConditionDescription : CitizenTransitionConditionDescription
    {
        private const string Equal = "Equal";
        private const string Greater = "Greater";
        private const string Less = "Less";
        
        private const string OperationNameKey = "operationName";
        private const string CounterNameKey = "counterName";
        private const string CounterValueKey = "value";

        private string CounterName { get; set; }

        private string OperationName { get; set; }

        private float Value { get; set; }
        
        public override bool Check(TempCitizenModel model)
        {
            switch (OperationName)
            {
                case Equal:
                {
                    return model.Counters[CounterName] == Value;
                }

                case Greater:
                {
                    Debug.Log($"Counter: {model.Counters[CounterName] > Value}");
                    return model.Counters[CounterName] > Value;
                }

                case Less:
                {
                    return model.Counters[CounterName] < Value;
                }
                
                default:
                {
                    return false;
                }
            }
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);   
            
            CounterName = data[CounterNameKey] as string;
            OperationName = data[OperationNameKey] as string;
            Value = Convert.ToSingle(data[CounterValueKey]);
        }
    }
}