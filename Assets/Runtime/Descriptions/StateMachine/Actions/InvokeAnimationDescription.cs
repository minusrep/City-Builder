using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class InvokeAnimationDescription : ActionDescription
    {
        private const string AnimationKey = "animation";
        
        public string Animation { get; private set; }
        
        public InvokeAnimationDescription(Dictionary<string, object> data) : base(data)
        {
            Animation = data.GetString(AnimationKey);
        }


        public override void Execute(World world, CitizenModel model)
        {
            model.InvokeAnimation(Animation);
        }
    }
}