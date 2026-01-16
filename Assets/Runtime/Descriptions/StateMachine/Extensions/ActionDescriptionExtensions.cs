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
        private const string SetBuildingPointOfInterestKey = "set_point_of_interest_building";
        private const string SetServiceCitizenAmountKey = "set_service_citizen_amount";
        private const string StartMoveKey = "start_move";
        private const string InvokeAnimationKey = "invoke_animation";
        private const string EnterBuildingKey = "enter_buidling";
        private const string RegisterSystemKey = "register_system";
        private const string UnregisterSystemKey = "unregister_system";
        private const string TakeResourceKey = "take_resource";
        private const string PutResourceKey = "put_resource";
        private const string SetFlagKey = "set_flag";
        private const string TakeOrderKey = "take_order";
        private const string ChangeVisibilityKey = "change_visibility";
        private const string StopMoveKey = "stop_move";
        
        public static ActionDescription ToActionDescription(this Dictionary<string, object> data)
        {
            return data[TypeKey] switch
            {
                TimerKey => new TimerActionDescription(data),
                SetPointOfInterestKey => new SetPointOfInterestActionDescription(data),
                SetBuildingPointOfInterestKey => new SetPointOfInterestBuildingActionDescription(data),
                StartMoveKey => new StartMoveActionDescription(data),
                EnterBuildingKey => new EnterBuildingActionDescription(data),
                RegisterSystemKey => new RegisterSystemActionDescription(data),
                UnregisterSystemKey => new UnregisterSystemActionDescription(data),
                InvokeAnimationKey => new InvokeAnimationDescription(data),
                TakeResourceKey => new TakeResourceActionDescription(data),
                PutResourceKey => new PutResourceActionDescription(data),
                SetFlagKey => new SetFlagActionDescription(data),
                TakeOrderKey => new TakeOrderActionDescription(data),
                ChangeVisibilityKey => new ChangeVisibilityActionDescription(data),
                StopMoveKey => new StopMoveActionDescription(data),
                SetServiceCitizenAmountKey => new SetServiceCitizenAmountDescription(data),
                _ =>  throw new NotImplementedException()
            };
        }
    }
}