using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingPresenter : BuildingPresenter<ServiceBuildingView>
    {
        private readonly ServiceBuildingSystem _serviceSystem;
        private readonly GameSystemCollection _gameSystemCollection;
        
        public ServiceBuildingPresenter(ServiceBuildingModel model, IBuildingViewPool viewPool, World world, WorldViewDescriptions worldViewDescriptions, GameSystemCollection gameSystemCollection) : base(model, viewPool, worldViewDescriptions)
        {
            _gameSystemCollection = gameSystemCollection;
            _serviceSystem = new ServiceBuildingSystem(model, world.Citizens);
        }

        public override void Enable()
        {
            base.Enable();

            _gameSystemCollection.Add(_serviceSystem);
        }

        public override void Disable()
        {
            base.Disable();
            
            _gameSystemCollection.Remove(_serviceSystem);
        }
    }
}