using System;
using System.Collections.Generic;
using Runtime.Descriptions.StateMachine.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public abstract class LogicConditionDescription : ConditionDescription
    {
        private const string ConditionsKey = "conditions";

        protected List<ConditionDescription> Conditions => _conditions;

        private readonly List<ConditionDescription> _conditions;

        protected LogicConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _conditions = new List<ConditionDescription>();

            if (!data.TryGetValue(ConditionsKey, out var rawConditions) ||
                rawConditions is not List<object> conditionsList)
            {
                throw new Exception("Conditions list expected");
            }

            foreach (var condition in conditionsList)
            {
                if (condition is not Dictionary<string, object> conditionDict)
                {
                    throw new Exception("Condition must be an object");
                }

                _conditions.Add(conditionDict.ToConditionDescription());
            }
        }
    }
}