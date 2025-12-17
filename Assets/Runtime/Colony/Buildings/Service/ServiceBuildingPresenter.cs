using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Service
{
    public class ServiceBuildingPresenter : BuildingPresenter<ServiceBuildingView>
    {
        private readonly ServiceBuildingSystem _serviceSystem;
        private readonly GameSystemCollection _gameSystemCollection;
        
        public ServiceBuildingPresenter(ServiceBuildingModel model, IBuildingViewPool viewPool, World world, ViewDescriptions.ViewDescriptions viewDescriptions, GameSystemCollection gameSystemCollection) : base(model, viewPool, viewDescriptions)
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