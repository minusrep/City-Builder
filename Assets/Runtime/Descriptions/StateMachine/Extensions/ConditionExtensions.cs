using System;
using System.Collections.Generic;
using Runtime.Descriptions.StateMachine.Conditions;

namespace Runtime.Descriptions.StateMachine.Extensions
{
    public static class ConditionExtensions
    {
        private const string TypeKey = "type";

        private const string CompareKey = "compare";
        private const string FlagKey = "flag";
        private const string Or = "or";
        private const string And = "and";
        private const string Distance = "distance";
        private const string Timer = "timer";
        private const string False = "false";
        private const string True = "true";
        private const string CitizenDistanceWithFlag = "citizen_distance_with_flag";
        private const string OrderExists = "order_exists";
        private const string BuildingWorking = "building_working";
        
        public static ConditionDescription ToConditionDescription(this Dictionary<string, object> data)
        {
            return data[TypeKey] switch
            {
                CompareKey => new CompareConditionDescription(data),
                FlagKey => new FlagConditionDescription(data),
                Or => new OrConditionDescription(data),
                And => new AndConditionDescription(data),
                Distance => new DistanceConditionDescription(data),
                Timer => new TimerConditionDescription(data),
                True => new TrueConditionDescription(data),
                False => new FalseConditionDescription(data),
                CitizenDistanceWithFlag => new CitizenDistanceWithFlagConditionDescription(data),
                OrderExists => new OrderExistsConditionDescription(data),
                BuildingWorking => new BuildingWorkingCondition(data),
                _ =>  throw new NotImplementedException()
            };
        }
    }
}