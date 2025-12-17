using System;
using System.Collections.Generic;
using Runtime.Descriptions.StateMachine.Actions;

namespace Runtime.Descriptions.StateMachine.Extensions
{
    public static class ActionDescriptionExtensions
    {
        private const string TypeKey = "type";
        private const string TimerKey = "set_timer";
        private const string SetPointOfInterestKey = "set_point_of_interest";
        private const string SetBuildingPointOfInterestKey = "set_building_point_of_interest";
        private const string StartMoveKey = "start_move";
        private const string EnterBuildingKey = "enter_buidling";
        
        public static ActionDescription ToActionDescription(this Dictionary<string, object> data)
        {
            return data[TypeKey] switch
            {
                TimerKey => new TimerActionDescription(data),
                SetPointOfInterestKey => new SetPointOfInterestActionDescription(data),
                SetBuildingPointOfInterestKey => new SetPointOfInterestActionDescription(data),
                StartMoveKey => new StartMoveActionDescription(data),
                EnterBuildingKey => new EnterBuildingActionDescription(data),
                _ =>  throw new NotImplementedException()
            };
        }
    }
}