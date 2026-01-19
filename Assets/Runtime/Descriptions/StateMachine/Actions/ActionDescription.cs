using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public abstract class ActionDescription 
    {
        public abstract void Execute(World world, CitizenModel model);
    }
}