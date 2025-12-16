using System;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Inventory;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter<ProductionBuildingView>
    {
        private readonly ProductionBuildingModel _model;
        private readonly GameSystemCollection _systemCollection;
        private readonly ProductionBuildingSystem _productionSystem;

        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IBuildingViewPool viewPool,
            ViewDescriptions.ViewDescriptions viewDescriptions, GameSystemCollection systemCollection) : base(model, viewPool, viewDescriptions)
        {
            _model = model;
            _systemCollection = systemCollection;
            _productionSystem = new ProductionBuildingSystem(_model);
        }

        public override void Enable()
        {
            base.Enable();

            _model.StartProduction(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            
            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                ViewDescriptions.InventoryViewDescription, View.Transform);

            _inventoryPresenter.Enable();
            
            _systemCollection.Add(_productionSystem);
            _productionSystem.OnUpdate += HandleUpdate;
        }

        public override void Disable()
        {
            _model.StopProduction();

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;

            _productionSystem.OnUpdate -= HandleUpdate;
            _systemCollection.Remove(_productionSystem);
            
            base.Disable();
        }
        
        private void HandleUpdate(long time)
        {
            var progress = (float)(time - _model.StartProductionTime) / _model.Description.ProductionTime;
            View.ProgressBar.value = Math.Clamp(progress, 0f, 1f) * 100f;
        }
    }
}