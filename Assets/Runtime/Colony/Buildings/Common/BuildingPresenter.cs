using Runtime.Colony.Buildings.Pool;
using Runtime.Common;
using Runtime.ViewDescriptions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPresenter<TView> : IPresenter where TView : BuildingView
    {
        private BuildingModel Model { get; }
        protected WorldViewDescriptions WorldViewDescriptions { get; }
        private IBuildingViewPool ViewPool { get; }
        protected TView View { get; private set; }

        public BuildingPresenter(BuildingModel model, IBuildingViewPool viewPool, WorldViewDescriptions worldViewDescriptions)
        {
            Model = model;
            WorldViewDescriptions = worldViewDescriptions;
            ViewPool = viewPool;
        }

        public virtual void Enable()
        {
            View = (TView)ViewPool.Get();
            View.Transform.position = ModelPositionToVector3(Model);

            Model.OnPositionChanged += HandlePositionChanged;
        }

        public virtual void Disable()
        {
            ViewPool.Release(View);
            View = null;
            Model.OnPositionChanged -= HandlePositionChanged;
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