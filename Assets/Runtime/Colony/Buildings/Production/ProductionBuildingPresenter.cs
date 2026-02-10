using System;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Inventory;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter
    {
        private readonly ProductionBuildingModel _model;
        private readonly World _world;

        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IObjectPool<BuildingView> viewPool,
            World world, WorldViewDescriptions worldViewDescriptions) : base(model, viewPool, worldViewDescriptions)
        {
            _model = model;
            _world = world;
        }

        public override void Enable()
        {
            base.Enable();
            
            var inventoryView = new InventoryView(View.Document.rootVisualElement, WorldViewDescriptions.InventoryViewDescription);
            
            _inventoryPresenter = new InventoryPresenter(inventoryView, _model.Inventory);

            _inventoryPresenter.Enable();
            
            ((ProductionBuildingSystem)_world.GameSystems.Get("production")).Register(_model);
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