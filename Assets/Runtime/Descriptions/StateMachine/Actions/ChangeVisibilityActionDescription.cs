using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class ChangeVisibilityActionDescription: ActionDescription
    {
        private readonly bool _isVisible;
        
        public ChangeVisibilityActionDescription(Dictionary<string, object> data) : base(data)
        {
            _isVisible = Convert.ToBoolean(data["visible"]);
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.SetVisibility(_isVisible);
        }
    }
}