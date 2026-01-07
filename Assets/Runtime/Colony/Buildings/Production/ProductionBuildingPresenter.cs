using System;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Common.ObjectPool;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter
    {
        private readonly ProductionBuildingModel _model;
        private readonly GameSystemCollection _systemCollection;
        private ProductionBuildingSystem _productionSystem;

        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IObjectPool<BuildingView> viewPool,
           WorldViewDescriptions worldViewDescriptions, GameSystemCollection systemCollection) : base(model, viewPool, worldViewDescriptions)
        {
            _model = model;
            _systemCollection = systemCollection;
        }

        public override void Enable()
        {
            base.Enable();
            
            _productionSystem = new ProductionBuildingSystem(_model.Id, _model, View);

            _model.StartProduction(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            
            _inventoryPresenter = new InventoryPresenter(_model.Inventory, View.Document, WorldViewDescriptions);

            _inventoryPresenter.Enable();
            
            _model.Inventory.OnRemoveItem += HandleRemovedResource;
            
            _systemCollection.Add(_productionSystem);
        }
        
        public override void Disable()
        {
            _model.StopProduction();

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;

            _model.Inventory.OnRemoveItem -= HandleRemovedResource;
            
            _systemCollection.Remove(_productionSystem);
            
            base.Disable();
        }

        private void HandleRemovedResource()
        {
            var currentAmount = _model.Inventory.Models[_model.Description.ProductionResource].Amount;
            if (_model.CapacityLeft() && currentAmount == _model.Description.MaxResource - 1)
            {
                _model.StartProduction(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
            }
        }
    }
}