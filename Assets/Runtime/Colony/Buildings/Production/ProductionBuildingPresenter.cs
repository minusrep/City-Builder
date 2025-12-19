using System;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Inventory;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter<ProductionBuildingView>
    {
        private readonly ProductionBuildingModel _model;
        private readonly GameSystemCollection _systemCollection;
        private ProductionBuildingSystem _productionSystem;

        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IBuildingViewPool viewPool,
           WorldViewDescriptions worldViewDescriptions, GameSystemCollection systemCollection) : base(model, viewPool, worldViewDescriptions)
        {
            _model = model;
            _systemCollection = systemCollection;
        }

        public override void Enable()
        {
            base.Enable();
            
            _productionSystem = new ProductionBuildingSystem(_model, View);

            _model.StartProduction(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            
            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                WorldViewDescriptions.InventoryViewDescription, View.Transform);

            _inventoryPresenter.Enable();
            
            _systemCollection.Add(_productionSystem);
        }

        public override void Disable()
        {
            _model.StopProduction();

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;

            _systemCollection.Remove(_productionSystem);
            
            base.Disable();
        }
    }
}