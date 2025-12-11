using Runtime.Descriptions.Citizens.Movement;
using Runtime.ModelCollections;
using Runtime.StateMachine.Conditions;

namespace Runtime.Colony.Citizens
{
    public interface ICitizenModel : 
        ITimerModel, IFlagsModel, IMovementModel, IStatsModel, 
        IUserConditionModel, ISerializeModel, IDeserializeModel
    {
        
    }
}