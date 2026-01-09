using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;
using Runtime.Descriptions.StateMachine.Conditions;
using Runtime.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class CitizenDistanceWithFlagConditionDescription : ConditionDescription
    {
        private const string DistanceValueKey = "distance_value";
        private const string FlagNameKey = "flag_name";
        private const string FlagValueKey = "flag_value";

        private readonly float _distanceValue;
        private readonly string _flagName;
        private readonly bool _flagValue;

        public CitizenDistanceWithFlagConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _distanceValue = data.GetFloat(DistanceValueKey);
            _flagName = data.GetString(FlagNameKey);
            _flagValue = data.GetBool(FlagValueKey);
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IMovementModel movementModel)
            {
                return false;
            }

            var from = movementModel.Position;

            foreach (var citizen in world.Citizens.Models.Values)
            {
                if (citizen == user)
                {
                    continue;
                }

                if (citizen.Flags.TryGetValue(_flagName, out bool value))
                {
                    if (value == _flagValue && Vector3.Distance(from, citizen.Position) <= _distanceValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
