using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public static class ConditionExtensions
    {
        private const string TypeKey = "type";
        
        private const string CompareKey = "compare";
        private const string FlagKey = "flag";
        private const string Or = "or";
        private const string And = "and";
        private const string Distance = "distance";
        
        public static ConditionDescription ToConditionDescription(this Dictionary<string, object> data)
        {
            return data[TypeKey] switch
            {
                CompareKey => CreateConditionDescription<CompareConditionDescription>(data),
                FlagKey => CreateConditionDescription<FlagConditionDescription>(data),
                Or => CreateConditionDescription<OrConditionDescription>(data),
                And => CreateConditionDescription<AndConditionDescription>(data),
                _ =>  throw new NotImplementedException()
            };
        }

        private static T CreateConditionDescription<T>(Dictionary<string, object> data) where T : ConditionDescription, new()
        {
            var conditionDescription = new T();
            
            conditionDescription.Deserialize(data);

            return conditionDescription;
        }
    }
}