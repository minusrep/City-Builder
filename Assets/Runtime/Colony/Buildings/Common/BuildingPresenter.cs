using Runtime.Common;
using Runtime.Common.ObjectPool;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPresenter : IPresenter
    {
        private BuildingModel Model { get; }
        protected ViewDescriptions.ViewDescriptions ViewDescriptions { get; }
        private ObjectPool<BuildingView> ViewPool { get; }
        protected BuildingView View { get; private set; }

        public BuildingPresenter(BuildingModel model, ObjectPool<BuildingView> viewPool, ViewDescriptions.ViewDescriptions viewDescriptions)
        {
            Model = model;
            ViewDescriptions = viewDescriptions;
            ViewPool = viewPool;
        }

        public virtual void Enable()
        {
            View = ViewPool.Get();
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