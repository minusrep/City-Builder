using Runtime.ViewDescriptions.Buildings;
using Runtime.Colony.Inventory;
using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public sealed class BuildingPresenter : IPresenter
    {
        private BuildingModel Model { get; }
        private BuildingViewDescription ViewDescription { get; }
        private Transform RootTransform { get; }
        private BuildingView View { get; set; }

        private InventoryPresenter _inventoryPresenter;

        public BuildingPresenter(BuildingModel model, BuildingViewDescription viewDescription, Transform rootTransform)
        {
            Model = model;
            ViewDescription = viewDescription;
            RootTransform = rootTransform;
        }

        public void Enable()
        {
            View = Object.Instantiate(ViewDescription.Prefab, RootTransform);
            View.Transform.position = ModelPositionToVector3(Model);

            Model.OnPositionChanged += HandlePositionChanged;
            
            _inventoryPresenter = new InventoryPresenter(Model.Inventory, 
                ViewDescription.InventoryViewDescription, View.Transform);
            
            _inventoryPresenter.Enable();
        }

        public void Disable()
        {
            Object.Destroy(View);
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