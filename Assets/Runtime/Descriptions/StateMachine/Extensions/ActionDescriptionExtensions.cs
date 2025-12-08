using System;
using System.Collections.Generic;
using Runtime.StateMachine.Descriptions.Actions;

namespace Runtime.StateMachine.Descriptions.Extensions
{
    public static class ActionDescriptionExtensions
    {
        private const string TypeKey = "type";
        
        private const string TimerKey = "set_timer";
        private const string SetPointOfInterestKey = "set_point_of_interest";
        
        public static ActionDescription ToActionDescription(this Dictionary<string, object> data)
        {
            return data[TypeKey] switch
            {
                TimerKey => CreateConditionDescription<TimerActionDescription>(data),
                SetPointOfInterestKey => CreateConditionDescription<SetPointOfInterestActionDescription>(data),
                _ =>  throw new NotImplementedException()
            };
        }

        private static T CreateConditionDescription<T>(Dictionary<string, object> data) where T : ActionDescription, new()
        {
            var conditionDescription = new T();
            
            conditionDescription.Deserialize(data);

            return conditionDescription;
        }
    }
}