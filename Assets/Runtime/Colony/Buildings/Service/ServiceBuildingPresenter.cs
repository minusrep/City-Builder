using Runtime.Colony.Buildings.Common;
using Runtime.Common.ObjectPool;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingPresenter : BuildingPresenter
    {
        public ServiceBuildingPresenter(ServiceBuildingModel model, IObjectPool<BuildingView> viewPool, World world, WorldViewDescriptions worldViewDescriptions) : base(model, viewPool, world, worldViewDescriptions)
        {

        }

        public override void Enable()
        {
            base.Enable();
        }

        public override void Disable()
        {
            base.Disable();
        }
    }
}