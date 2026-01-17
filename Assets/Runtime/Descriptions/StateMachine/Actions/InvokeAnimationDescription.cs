using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class InvokeAnimationDescription : ActionDescription
    {
        private const string AnimationKey = "animation";

        private string Animation { get; }

        public InvokeAnimationDescription(Dictionary<string, object> data)
        {
            Animation = data.GetString(AnimationKey);
        }


        public override void Execute(World world, CitizenModel model)
        {
            model.InvokeAnimation(Animation);
        }
    }
}