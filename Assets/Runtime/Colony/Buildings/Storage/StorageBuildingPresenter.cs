using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Inventory;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Storage
{
    public class StorageBuildingPresenter : BuildingPresenter<StorageBuildingView>
    {
        private readonly StorageBuildingModel _model;
        
        private InventoryPresenter _inventoryPresenter;
        
        public StorageBuildingPresenter(StorageBuildingModel model, IBuildingViewPool viewPool, WorldViewDescriptions worldViewDescriptions) : base(model, viewPool, worldViewDescriptions)
        {
            _model = model;
        }
        
        public override void Enable()
        {
            base.Enable();

            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                WorldViewDescriptions.InventoryViewDescription, View.Transform);

            _inventoryPresenter.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;
        }
    }
}