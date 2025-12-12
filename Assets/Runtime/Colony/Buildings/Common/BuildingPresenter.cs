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
        protected BuildingViewDescription ViewDescription { get; }
        private ObjectPool<BuildingView> ViewPool { get; }
        protected BuildingView View { get; set; }

        public BuildingPresenter(BuildingModel model, ObjectPool<BuildingView> viewPool, BuildingViewDescription viewDescription)
        {
            Model = model;
            ViewDescription = viewDescription;
            ViewPool = viewPool;
        }

        public virtual void Enable()
        {
            View = ViewPool.Get();
            View.Transform.position = ModelPositionToVector3(Model);

            Model.OnPositionChanged += HandlePositionChanged;
            
            _inventoryPresenter = new InventoryPresenter(Model.Inventory, 
                ViewDescription.InventoryViewDescription, View.Transform);
            
            _inventoryPresenter.Enable();
        }

        public virtual void Disable()
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