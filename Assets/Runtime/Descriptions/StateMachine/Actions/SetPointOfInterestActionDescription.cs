using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetPointOfInterestActionDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";

        private string PointOfInterest { get; }

        public SetPointOfInterestActionDescription(Dictionary<string, object> data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var point = world.WorldDescription.PointOfInterestCollection.Get(PointOfInterest);

            model.SetPointOfInterest(PointOfInterest, point);
        }
    }
}