using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetPointOfInterestActionDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";

        public string PointOfInterest { get; private set; }

        public SetPointOfInterestActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }

        public override void Execute(World world, CitizenModel model)
        {
            var point = world.WorldDescription.PointOfInterestCollection.Get(PointOfInterest);
 
            model.SetPointOfInterest(PointOfInterest, point);
        }
    }
}