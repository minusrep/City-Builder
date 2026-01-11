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

        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IObjectPool<BuildingView> viewPool,
            World world,
            WorldViewDescriptions worldViewDescriptions) : base(model, viewPool,
            world, worldViewDescriptions)
        {
            _model = model;
        }

        public override void Enable()
        {
            base.Enable();
            
            _inventoryPresenter = new InventoryPresenter(_model.Inventory, View.Document, WorldViewDescriptions);

            _inventoryPresenter.Enable();

            _model.OnProgressChanged += OnProgressChanged;
            
            _model.StartProduction();
            
            OnProgressChanged(_model.Progress);
        }

        public override void Disable()
        {
            _model.OnProgressChanged -= OnProgressChanged;

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;
            
            base.Disable();
        }
        
        private void OnProgressChanged(float progress)
        {
            View.ProgressBar.value = Math.Clamp(progress, 0f, 1f) * 100f;
        }
    }
}