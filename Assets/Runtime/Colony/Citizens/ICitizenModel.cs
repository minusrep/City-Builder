using Runtime.Core;
using Runtime.ModelCollections;

namespace Runtime.Colony.Citizens
{
    public interface ICitizenModel : 
        ITimerModel, IFlagsModel, IMovementModel, IStatsModel, 
        IConditionModel, ISerializeModel, IDeserializeModel
    {
        
    }
}