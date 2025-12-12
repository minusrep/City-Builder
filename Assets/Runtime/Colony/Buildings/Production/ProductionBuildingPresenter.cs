using Runtime.ViewDescriptions.Buildings;
using Runtime.Colony.Buildings.Common;
using Runtime.Common.ObjectPool;
using Runtime.Colony.Inventory;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter
    {
        private readonly ProductionBuildingModel _model;
        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, ObjectPool<BuildingView> viewPool,
            BuildingViewDescription viewDescription) : base(model, viewPool, viewDescription)
        {
        }

        public override void Enable()
        {
            base.Enable();

            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                ViewDescription.InventoryViewDescription, View.Transform);

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