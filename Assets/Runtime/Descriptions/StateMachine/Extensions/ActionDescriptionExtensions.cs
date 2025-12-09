using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions
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
                TimerKey => new TimerActionDescription(data),
                SetPointOfInterestKey => new SetPointOfInterestActionDescription(data),
                _ =>  throw new NotImplementedException()
            };
        }
    }
}