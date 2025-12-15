using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Inventory;

namespace Runtime.Colony.Buildings.Storage
{
    public class StorageBuildingPresenter : BuildingPresenter<StorageBuildingView>
    {
        private readonly StorageBuildingModel _model;
        
        private InventoryPresenter _inventoryPresenter;
        
        public StorageBuildingPresenter(StorageBuildingModel model, IBuildingViewPool viewPool, ViewDescriptions.ViewDescriptions viewDescriptions) : base(model, viewPool, viewDescriptions)
        {
            _model = model;
        }
        
        public override void Enable()
        {
            base.Enable();

            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                ViewDescriptions.InventoryViewDescription, View.Transform);

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