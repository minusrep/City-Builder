using Runtime.ViewDescriptions.Buildings;
using Runtime.Colony.Inventory;
using Runtime.Common;
using Runtime.Common.ObjectPool;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPresenter : IPresenter
    {
        private BuildingModel Model { get; }
        private BuildingViewDescription ViewDescription { get; }
        private ObjectPool<BuildingView> ViewPool { get; }
        private BuildingView View { get; set; }

        private InventoryPresenter _inventoryPresenter;

        public BuildingPresenter(BuildingModel model, ObjectPool<BuildingView> viewPool, BuildingViewDescription viewDescription)
        {
            Model = model;
            ViewDescription = viewDescription;
            ViewPool = viewPool;
        }

        public void Enable()
        {
            View = ViewPool.Get();
            View.Transform.position = ModelPositionToVector3(Model);

            Model.OnPositionChanged += HandlePositionChanged;
            
            _inventoryPresenter = new InventoryPresenter(Model.Inventory, 
                ViewDescription.InventoryViewDescription, View.Transform);
            
            _inventoryPresenter.Enable();
        }

        public void Disable()
        {
            ViewPool.Release(View);
            View = null;
            Model.OnPositionChanged -= HandlePositionChanged;
            
            _inventoryPresenter.Disable();
        }

        private void HandlePositionChanged()
        {
            View.Transform.position = ModelPositionToVector3(Model);
        }

        private Vector3 ModelPositionToVector3(BuildingModel model)
        {
            return new Vector3(model.Position.x, 0f, model.Position.y);
        }
    }
}