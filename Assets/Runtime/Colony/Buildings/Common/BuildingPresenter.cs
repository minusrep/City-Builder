using Runtime.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPresenter : IPresenter
    {
        private BuildingModel Model { get; }
        protected WorldViewDescriptions WorldViewDescriptions { get; }
        private IObjectPool<BuildingView> ViewPool { get; }
        protected BuildingView View { get; private set; }

        public BuildingPresenter(BuildingModel model, IObjectPool<BuildingView> viewPool, WorldViewDescriptions worldViewDescriptions)
        {
            Model = model;
            WorldViewDescriptions = worldViewDescriptions;
            ViewPool = viewPool;
        }

        public virtual void Enable()
        {
            View = ViewPool.Get();
            View.Initialize();
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
            return new Vector3(model.WorldPosition.x, 0f, model.WorldPosition.y);
        }
    }
}