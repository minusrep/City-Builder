using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class StartMoveActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";

        private string PointOfInterest { get; }
        
        public StartMoveActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.StartMove(PointOfInterest);
        }
    }
}