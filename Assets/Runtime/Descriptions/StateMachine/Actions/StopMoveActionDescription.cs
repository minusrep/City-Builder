using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class StopMoveActionDescription : ActionDescription
    {
        public StopMoveActionDescription(Dictionary<string, object> data) : base(data)
        {
            
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.StopMove();
        }
    }
}