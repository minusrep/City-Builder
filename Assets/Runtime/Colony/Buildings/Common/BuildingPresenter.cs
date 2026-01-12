using Runtime.Colony.Buildings.Construction;
using Runtime.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPresenter : IPresenter
    {
        protected WorldViewDescriptions WorldViewDescriptions { get; }
        protected BuildingView View { get; private set; }
        private BuildingModel Model { get; }
        private IObjectPool<BuildingView> ViewPool { get; }
        private BuildingViewDescription ViewDescription { get; }

        public BuildingPresenter(BuildingModel model, IObjectPool<BuildingView> viewPool,
            WorldViewDescriptions worldViewDescriptions)
        {
            Model = model;
            WorldViewDescriptions = worldViewDescriptions;
            ViewDescription =
                WorldViewDescriptions.BuildingViewDescriptions.Get(Model.BaseDescription.ViewDescriptionId);
            ViewPool = viewPool;
        }

        public virtual void Enable()
        {
            View = ViewPool.Get();
            HandlePositionChanged();
            View.Transform.localScale = BuildingVisualLayoutHelper.GetScale(ViewDescription);

            View.Id = Model.Id;

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
            return new Vector3(model.WorldPosition.x, 0f, model.WorldPosition.y);
        }
    }
}