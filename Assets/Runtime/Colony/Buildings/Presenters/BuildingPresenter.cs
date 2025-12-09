using Runtime.ViewDescriptions.Buildings;
using Runtime.Colony.Buildings.Models;
using Runtime.Colony.Buildings.Views;
using Runtime.Common;
using UnityEngine;
using IPresenter = Runtime.Core.IPresenter;

namespace Runtime.Colony.Buildings.Presenters
{
    public sealed class BuildingPresenter : IPresenter
    {
        private BuildingModel Model { get; }
        private BuildingView View { get; }

        public BuildingPresenter(BuildingModel model, BuildingViewDescription viewDescription, Transform parent)
        {
            Model = model;
            
            View = Object.Instantiate(viewDescription.Prefab, parent);
            View.gameObject.SetActive(false);
        }
        
        public void Enable()
        {
            View.SetActive(true);
            View.SetPosition(ModelPositionToVector3(Model));
        }

        public void Disable()
        {
            View.SetActive(false);
        }

        private Vector3 ModelPositionToVector3(BuildingModel model)
        {
            return new Vector3(model.Position.x, 0f, model.Position.y);
        }
    }
}