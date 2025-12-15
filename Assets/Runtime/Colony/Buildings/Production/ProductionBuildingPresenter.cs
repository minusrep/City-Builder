using System;
using System.Timers;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Inventory;

namespace Runtime.Colony.Buildings.Production
{
    public class ProductionBuildingPresenter : BuildingPresenter<ProductionBuildingView>
    {
        private readonly ProductionBuildingModel _model;
        private Timer _timer;
        
        private InventoryPresenter _inventoryPresenter;

        public ProductionBuildingPresenter(ProductionBuildingModel model, IBuildingViewPool viewPool,
            ViewDescriptions.ViewDescriptions viewDescriptions) : base(model, viewPool, viewDescriptions)
        {
            _model = model;
            _timer = new Timer(1000);
            _timer.AutoReset = true;
        }

        public override void Enable()
        {
            base.Enable();
            
            _model.StartProduction(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            _inventoryPresenter = new InventoryPresenter(_model.Inventory,
                ViewDescriptions.InventoryViewDescription, View.Transform);

            _inventoryPresenter.Enable();
            
            _timer.Elapsed += Update;
            _timer.Start();
        }

        public override void Disable()
        {
            base.Disable();
            
            _model.StopProduction();

            _inventoryPresenter.Disable();
            _inventoryPresenter = null;
            
            _timer.Elapsed -= Update;
            _timer.Dispose();
            _timer = null;
        }
        
        private void Update(object sender, ElapsedEventArgs e)
        {
            _model.Update(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        }
    }
}