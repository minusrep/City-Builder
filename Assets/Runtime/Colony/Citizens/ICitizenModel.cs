using Runtime.Colony.StateMachine.Conditions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Citizens
{
    public interface ICitizenModel : 
        ITimerModel, IFlagsModel, IMovementModel, IStatsModel, 
        IUserConditionModel, ISerializeModel, IDeserializeModel
    {
        
    }
}